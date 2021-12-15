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
            EmailAddr.PreviewTouchDown += (object sender, TouchEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            PhoneNumber.PreviewTouchDown += (object sender, TouchEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            PreviewTouchDown += (object sender, TouchEventArgs e) =>
            {
                renderView();
            };

            EmailAddr.MouseDown += (object sender, MouseButtonEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            PhoneNumber.MouseDown += (object sender, MouseButtonEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            MouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                renderView();
            };
        }

        private void renderView()
        {
            Osklib.OnScreenKeyboard.Close();
        }


    }
}
