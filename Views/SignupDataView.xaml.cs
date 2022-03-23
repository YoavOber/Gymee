using GymeeDesktopApp.ViewModels;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GymeeDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class SignupDataView : UserControl
    {
        public SignupDataView()
        {
            InitializeComponent();
            //Password.Password = "";
            //ShowPassTxtBox.Text = "";
            //  CheckBx.Visibility = Visibility.Hidden;
            FullName.PreviewTouchDown += OpenOSK;
            Password.PreviewTouchDown += OpenOSK;
            EmailAddr.PreviewTouchDown += OpenOSK;
            PhoneNumber.PreviewTouchDown += OpenOSK;
            ShowPassTxtBox.PreviewTouchDown += OpenOSK;
            PreviewTouchDown += (object sender, TouchEventArgs e) =>
            {
                renderView();
            };

            FullName.PreviewMouseDown += OpenOSK;
            Password.PreviewMouseDown += OpenOSK;
            EmailAddr.PreviewMouseDown += OpenOSK;
            PhoneNumber.PreviewMouseDown += OpenOSK;
            ShowPassTxtBox.PreviewMouseDown += OpenOSK;
            PreviewMouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                renderView();
            };


        }

        public void reset()
        {
            FullName.Text = "";
            Password.Password = "";
            ShowPassTxtBox.Text = "";
            PhoneNumber.Text = "";
            EmailAddr.Text = "";
        }


        private void renderView()
        {
            Osklib.OnScreenKeyboard.Close();
        }

        public void OpenOSK(object sender, EventArgs e)
        {
            if (!Osklib.OnScreenKeyboard.IsOpened())
                Osklib.OnScreenKeyboard.Show();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignupDetailsViewModel)DataContext).Password = ((PasswordBox)sender).Password;
        }
        private void ShowPassTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((SignupDetailsViewModel)DataContext).Password = ((TextBox)sender).Text;
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ShowPassTxtBox.Text = Password.Password;
            Password.Visibility = Visibility.Collapsed;
            ShowPassTxtBox.Visibility = Visibility.Visible;
            CheckBx.Content = "הסתר סיסמא";
        }
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            Password.Password = ShowPassTxtBox.Text;
            Password.Visibility = Visibility.Visible;
            ShowPassTxtBox.Visibility = Visibility.Collapsed;
            CheckBx.Content = "הצג סיסמא";
        }

    }
}
