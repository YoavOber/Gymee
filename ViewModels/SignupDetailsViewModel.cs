using System.Net.Mail;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Toolkit.Mvvm;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using GymeeDestkopApp.Models;
using GymeeDestkopApp.Services;

namespace GymeeDesktopApp.ViewModels
{
    public class SignupDetailsViewModel : ObservableObject
    {
        private StrongReferenceMessenger Messanger { get; set; } = StrongReferenceMessenger.Default;
        public SignupDetailsViewModel()
        {
            Reset();
            MoveNextCmd = new RelayCommand(async () =>
            {
                EmailExists = false;
                IsLoading = true;
                EmailExists = await GymeeAuthenticateService.userExists(EmailAddr.ToLower());
                IsLoading = false;
                if (EmailExists)
                    return;
                Messanger.Send(new ChangePageMessage(PageIndex.SIGNUP_BODY_DATA));
                Messanger.Send(new SignupVMMessage(SignupProperty.Credentiels
                    , new UserCredentials
                    {
                        FullName = this.FullName,//'this' can be ommitted, used for clarity
                        EmailAddr = this.EmailAddr,
                        PhoneNumber = this.PhoneNumber,
                        Password = this.Password
                    }));
            });

            UserExistsCmd = new RelayCommand(() =>
            {
                Messanger.Send(new ChangePageMessage(PageIndex.LOGIN));
            });
            GoBackCmd = new RelayCommand(() =>
            {
                PerformGoBackCmd();
            });
            Messanger.Register<SignupDetailsViewModel, string>(this, (r, m) =>
            {
                if (m == "resetVM")
                    Reset();
            });
        }

        ~SignupDetailsViewModel()
        {
            Reset();
        }

        private void Reset()
        {
            CanMoveNext = false;
            FullName = null;
            EmailAddr = null;
            Password = null;
            PhoneNumber = null;
            EmailExists = false;
            IsValidPhone = true;
            IsValidPassword = true;
            IsValidEmail = true;
            IsValidFullName = true;
        }

        #region Properties
        private string fullName;//user full name
        public string FullName
        {
            get => fullName;
            set
            {
                SetProperty(ref fullName, value);
                IsValidFullName = !string.IsNullOrWhiteSpace(FullName);
                CanMoveNext = IsValidFullName && IsValidEmail && IsValidPhone && IsValidPassword;
            }
        }

        private bool isLoading;

        public bool IsLoading { get => isLoading; set => SetProperty(ref isLoading, value); }

        private string emailAddr;//user email
        public string EmailAddr
        {
            get => emailAddr;
            set
            {
                SetProperty(ref emailAddr, value);
                try
                {
                    var email = new MailAddress(EmailAddr).Address;
                    IsValidEmail = EmailAddr == email;
                }
                catch
                {
                    IsValidEmail = false;
                }
                CanMoveNext = IsValidFullName && IsValidEmail && IsValidPhone && IsValidPassword;
            }
        }

        private string phoneNumber;//user email
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                SetProperty(ref phoneNumber, value);
                IsValidPhone = PhoneNumber != null && Regex.IsMatch(PhoneNumber, "^05[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");
                CanMoveNext = IsValidFullName && IsValidEmail && IsValidPhone && IsValidPassword;
            }
        }

        private string password;//user email
        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                IsValidPassword = !string.IsNullOrWhiteSpace(Password) && Password.Length >= 6;
                CanMoveNext = IsValidFullName && IsValidEmail && IsValidPhone && IsValidPassword;
            }
        }
        #endregion


        #region Booleans
        private bool isValidName;
        public bool IsValidFullName
        {
            get => isValidName;
            set => SetProperty(ref isValidName, value);
        }


        private bool isValidPassword;
        public bool IsValidPassword
        {
            get => isValidPassword;
            set => SetProperty(ref isValidPassword, value);
        }


        private bool isValidEmail;
        public bool IsValidEmail
        {
            get => isValidEmail;
            set => SetProperty(ref isValidEmail, value);
        }

        private bool isValidPhone;
        public bool IsValidPhone
        {
            get => isValidPhone;
            set => SetProperty(ref isValidPhone, value);
        }

        private bool canMoveNext;
        public bool CanMoveNext
        {
            get => canMoveNext;
            set
            {
                if (FullName != null && EmailAddr != null && PhoneNumber != null && Password != null)
                    SetProperty(ref canMoveNext, value);
            }
        }

        private bool emailExists;

        public bool EmailExists
        {
            get => emailExists;
            set => SetProperty(ref emailExists, value);
        }
        #endregion

        #region Commands
        public ICommand MoveNextCmd { get; set; }
        public ICommand UserExistsCmd { get; set; }

        public ICommand GoBackCmd { get; set; }

        private void PerformGoBackCmd()
        {
            Messanger.Send(new ChangePageMessage(PageIndex.TERMS_OF_USE));
        }

        #endregion
    }
}
