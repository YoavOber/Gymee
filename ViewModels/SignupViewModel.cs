using GymeeDesktopApp.Models;
using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GymeeDestkopApp.Services;
using System.Threading.Tasks;

namespace GymeeDestkopApp.ViewModels
{
    public class SignupViewModel : ObservableObject
    {
        public FitnessLevel FitnessLevel { get; set; }
        public Goal Goal { get; set; }
        public WeeklyWorkouts WeeklyWorkouts { get; set; }
        public UserCredentials UserCredentials { get; set; }

        private bool canMoveNext;
        public bool CanMoveNext
        {
            get => canMoveNext;
            set => SetProperty(ref canMoveNext, value);
        }

        private int transitionerIndex;
        public int TransitionerIndex
        {
            get => transitionerIndex;
            set => SetProperty(ref transitionerIndex, value);
        }

        private int wheelSelectorIndex;
        public int WheelSelectorIndex
        {
            get => wheelSelectorIndex;
            set => SetProperty(ref wheelSelectorIndex, value);
        }


        private double progress;
        public double Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        private int selectedHeight;
        public int SelectedHeight
        {
            get => selectedHeight;
            set
            {
                SetProperty(ref selectedHeight, value);
                CanMoveNext = SelectedHeight != -1 && SelectedWeight != -1 && GenderList.Contains(SelectedGender) && DateStr.Contains("/");
            }
        }


        private int selectedWeight;
        public int SelectedWeight
        {
            get => selectedWeight;
            set
            {
                SetProperty(ref selectedWeight, value);
                CanMoveNext = SelectedHeight != -1 && SelectedWeight != -1 && GenderList.Contains(SelectedGender) && DateStr.Contains("/");
            }
        }

        //if you change the genderlist you need to change the relevant Gender selection code
        public string[] GenderList { get; set; } = { "נקבה", "זכר" };


        public string selectedGender;//גבר,אישה,אחר
        public string SelectedGender
        {
            get => selectedGender;
            set
            {
                SetProperty(ref selectedGender, value);
                CanMoveNext = SelectedHeight != -1 && SelectedWeight != -1 && GenderList.Contains(SelectedGender) && DateStr.Contains("/");
            }
        }


        private int bdday;
        public int BDDay
        {
            get => bdday;
            set
            {
                SetProperty(ref bdday, value);
                if (BDMonth > 0 && BDYear > 0)
                    DateStr = $"{BDDay}/{BDMonth}/{BDYear}";
            }
        }

        private int bdmonth;
        public int BDMonth
        {
            get => bdmonth;
            set
            {
                SetProperty(ref bdmonth, value + 1);//+1 since index binding is used and not value
                if (BDDay > 0 && BDYear > 0)
                    DateStr = $"{BDDay}/{BDMonth}/{BDYear}";
            }
        }
        private int bdyear;
        public int BDYear
        {
            get => bdyear;
            set
            {
                SetProperty(ref bdyear, value);
                if (BDMonth > 0 && BDDay > 0)
                    DateStr = $"{BDDay}/{BDMonth}/{BDYear}";
            }
        }
        private string dateStr;
        public string DateStr
        {
            get => dateStr;
            set
            {
                SetProperty(ref dateStr, value);
                CanMoveNext = SelectedHeight != -1 && SelectedWeight != -1 && GenderList.Contains(SelectedGender) && DateStr.Contains("/");
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }

        private bool isLoading;

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public ICommand MoveNextCmd { get; set; }
        public ICommand ShowSelectionWheelCmd { get; set; }
        public ICommand GoBackCmd { get; set; }
        public ICommand GoHomeCmd { get; set; }

        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;

        public SignupViewModel()
        {
            Reset();
            Messenger.Register<SignupViewModel, SignupVMMessage>(this, (r, m) => r.Receive(m));

            Messenger.Register<SignupViewModel, string>(this, (r, m) =>
            {
                if (m == "resetVM")
                    Reset();
            });

            Progress = 25;
            MoveNextCmd = new RelayCommand(async () => await MoveNext());
            ShowSelectionWheelCmd = new RelayCommand<string>(param => ShowSelectionWheel(param));
            GoBackCmd = new RelayCommand(() => PerformGoBackCmd());
            GoHomeCmd = new RelayCommand(() => { Messenger.Send(new ChangePageMessage(PageIndex.INTRO_PAGE)); });

        }

        public async Task MoveNext()
        {
            if (Progress < 100)
            {
                TransitionerIndex++;
                CanMoveNext = false;
                switch (TransitionerIndex)
                {
                    case 0:
                        if (FitnessLevel != FitnessLevel.NULL)
                            CanMoveNext = true;
                        break;
                    case 1:
                        if (Goal != Goal.NULL)
                            CanMoveNext = true;
                        break;
                    case 2:
                        if (WeeklyWorkouts != WeeklyWorkouts.NULL)
                            CanMoveNext = true;
                        break;
                    case 3:
                        CanMoveNext = SelectedHeight != -1 && SelectedWeight != -1 &&
                                      GenderList.Contains(SelectedGender) && DateStr.Contains("/");
                        break;
                }
                Progress += 25;
            }
            else
            {
                Gender gender;
                switch (selectedGender)
                {
                    case "זכר":
                        gender = Gender.Male;
                        break;
                    case "נקבה":
                        gender = Gender.Female;
                        break;
                    default:
                        gender = Gender.Other;
                        break;
                }

                var user = new User
                {
                    FullName = UserCredentials.FullName,
                    Branch = "",//ConfigurationService.GetConfiguration().Branch,
                    Email = UserCredentials.EmailAddr,
                    PhoneNumber = UserCredentials.PhoneNumber,
                    Password = UserCredentials.Password,
                    DateOfBirth = new DateTime(bdyear, bdmonth, bdday),
                    FitnessGoals = Goal,
                    FitnessLevel = this.FitnessLevel,//'this' for clarity,unnecessary
                    Height = (uint)selectedHeight,
                    Weight = SelectedWeight,
                    Injuries = new Injury[] { },
                    WeeklyWorkouts = this.WeeklyWorkouts,
                    Gender = gender
                };

                ErrorMessage = string.Empty;
                IsLoading = true;
                var signUpResult = await GymeeAuthenticateService.SignUp(user);
                IsLoading = false;
                if (signUpResult.success)
                {
                    var toLoginResult = new GymeeAuthenticateService.LoginResult //used by workout view
                    {
                        email = UserCredentials.EmailAddr,
                        name = UserCredentials.FullName,
                        fitnessLevel = FitnessLevel.Beginner //on sign up - user is always a beginner
                    };
                    Messenger.Send(new ChangePageMessage(PageIndex.PRE_WORKOUT,toLoginResult));
                    return;
                }
                else
                {
                    ErrorMessage = "תקלה בהרשמה,יש לנסות שנית עם פרטים אחרים";//signUpResult.error; 
                }
            }
        }
        ~SignupViewModel()
        {
            Messenger.UnregisterAll(this);
        }

        private void Reset()
        {
            Progress = 25;
            CanMoveNext = false;
            TransitionerIndex = 0;
            WheelSelectorIndex = -1;
            SelectedHeight = -1;
            SelectedWeight = -1;
            SelectedGender = "לא הוזן";
            BDDay = -1;
            BDMonth = -1;
            BDYear = -1;
            DateStr = "לא הוזן";
            ErrorMessage = string.Empty;
            FitnessLevel = FitnessLevel.NULL;
            Goal = Goal.NULL;
            WeeklyWorkouts = WeeklyWorkouts.NULL;
        }

        public delegate void ChangeScrollerEvent(string scrollerChanegd);//delegate ued by MAINWINDOW to know to switch between pages
        public static ChangeScrollerEvent OnChangeScreen { get; set; }


        private void ShowSelectionWheel(string cmd)
        {
            switch (cmd)
            {
                case "height":
                    WheelSelectorIndex = 0;
                    OnChangeScreen.Invoke("height");
                    break;
                case "weight":
                    WheelSelectorIndex = 1;
                    OnChangeScreen.Invoke("weight");
                    break;
                case "gender":
                    WheelSelectorIndex = 2;
                    break;
                case "age":
                    WheelSelectorIndex = 3;
                    OnChangeScreen.Invoke("age");
                    break;
                case "collapse":
                default:
                    WheelSelectorIndex = -1;
                    break;
            }
        }

        public void Receive(SignupVMMessage message)
        {
            if (message.Data == null)
                Reset();
            else switch (message.SignupDetail)
                {
                    case SignupProperty.FitnessLevel:
                        FitnessLevel = (FitnessLevel)message.Data;
                        CanMoveNext = true;
                        break;
                    case SignupProperty.Goal:
                        Goal = (Goal)message.Data;
                        CanMoveNext = true;
                        break;
                    case SignupProperty.WorkoutDays:
                        WeeklyWorkouts = (WeeklyWorkouts)message.Data;
                        CanMoveNext = true;
                        break;
                    case SignupProperty.Credentiels:
                        UserCredentials = (UserCredentials)message.Data;
                        break;
                    case SignupProperty.Height:
                        SelectedHeight = (int)message.Data;
                        break;
                    case SignupProperty.Weight:
                        SelectedWeight = (int)message.Data;
                        break;
                    case SignupProperty.BdDay:
                        BDDay = (int)message.Data;
                        break;
                    case SignupProperty.BdMonth:
                        BDMonth = (int)message.Data;
                        break;
                    case SignupProperty.BdYear:
                        BDYear = (int)message.Data;
                        break;
                    case SignupProperty.Gender:
                        SelectedGender = (string)message.Data;
                        break;
                    default:
                        break;
                }
        }

        private void PerformGoBackCmd()
        {
            if (TransitionerIndex > 0)
            {
                TransitionerIndex--;
                CanMoveNext = true;
                Progress -= 25;
                ErrorMessage = string.Empty;
            }
            else
            {
                Messenger.Send(new ChangePageMessage(PageIndex.SIGNUP_DATA));
            }
        }


    }
}
