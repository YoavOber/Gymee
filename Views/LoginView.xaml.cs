using GymeeDesktopApp.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace GymeeDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            EmailAddr.PreviewTouchDown += (object sender, TouchEventArgs e) => { openOSK(); };
            PhoneNumber.PreviewTouchDown += (object sender, TouchEventArgs e) => { openOSK(); };
            PreviewTouchDown += (object sender, TouchEventArgs e) =>
            {
                renderView();
            };

            EmailAddr.MouseDown += (object sender, MouseButtonEventArgs e) => { openOSK(); };
            PhoneNumber.MouseDown += (object sender, MouseButtonEventArgs e) => { openOSK(); };
            MouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                renderView();
            };
        }

        private void renderView()
        {
            Osklib.OnScreenKeyboard.Close();
        }

        private void openOSK()
        {
           // if (!Osklib.OnScreenKeyboard.IsOpened())
                Osklib.OnScreenKeyboard.Show();
        }

    }
}
