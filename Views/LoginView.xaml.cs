using GymeeDesktopApp.ViewModels;
using System;
using System.Drawing;
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
            EmailAddr.PreviewTouchDown += OpenOSK;
            PhoneNumber.PreviewTouchDown += OpenOSK;
            PreviewTouchDown += (object sender, TouchEventArgs e) =>
            {
                renderView();
            };

            EmailAddr.PreviewMouseDown += OpenOSK;
            PhoneNumber.PreviewMouseDown += OpenOSK;
            MouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                renderView();
            };
        }

        private void renderView()
        {
            Osklib.OnScreenKeyboard.Close();
        }


        public void OpenOSK(object sender, EventArgs e) //maybe remove
        {
            if (!Osklib.OnScreenKeyboard.IsOpened())
                Osklib.OnScreenKeyboard.Show();
        }
    }
}
