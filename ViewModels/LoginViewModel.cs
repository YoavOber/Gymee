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
    public class LoginViewModel : ObservableObject
    {
        #region Properties
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
                CanMoveNext = IsValidEmail && IsValidPhone;
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
                CanMoveNext = IsValidEmail && IsValidPhone;
            }
        }


        #endregion

        public ICommand NewUserCmd { get; set; } //new user btn command
        public ICommand LoginCmd { get; set; } //login command 
        public ICommand GoBackCmd { get; set; }

        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;
        public LoginViewModel()
        {
            Reset();
            NewUserCmd = new RelayCommand(() =>
            {
                Messenger.Send(new ChangePageMessage(PageIndex.SIGNUP_DATA));
            });
            GoBackCmd = new RelayCommand(() => PerformGoBackCmd());
            LoginCmd = new RelayCommand(async () =>
            {
                IsLoading = true;
                UserExists = true;
                var result = await GymeeAuthenticateService.Login(PhoneNumber, EmailAddr.ToLower());
                UserExists = result.loggedIn;
                //note - email is tolower.
                IsLoading = false;
                if (UserExists)
                {
                    
                    Messenger.Send(new ChangePageMessage(PageIndex.PRE_WORKOUT,result));
                    return;
                }
            });
            Messenger.Register<LoginViewModel, string>(this, (r, m) =>
            {
                if (m == "resetVM")
                    Reset();
            });
            Messenger.Register<LoginViewModel, ChangePageMessage>(this, (r, m) =>
            {
                if (m.Index == PageIndex.LOGIN)
                    Reset();
            });
        }

        private void Reset()
        {
            CanMoveNext = false;
            EmailAddr = null;
            PhoneNumber = null;
            IsValidEmail = true;
            IsValidPhone = true;
            UserExists = true;
        }

        private bool userExists;
        public bool UserExists
        {
            get => userExists;
            set => SetProperty(ref userExists, value);
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
                if (EmailAddr != null && PhoneNumber != null)
                    SetProperty(ref canMoveNext, value);
            }

        }

        private bool isLoading;

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }


        private void PerformGoBackCmd()
        {
            Messenger.Send(new ChangePageMessage(PageIndex.START_SIGNUP_OR_LOGIN));
        }
    }
}
