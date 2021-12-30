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
            FullName.PreviewTouchDown += (object sender, TouchEventArgs e) => { openOSK(); };
            Password.PreviewTouchDown += (object sender, TouchEventArgs e) => { openOSK(); };
            EmailAddr.PreviewTouchDown += (object sender, TouchEventArgs e) => { openOSK(); };
            PhoneNumber.PreviewTouchDown += (object sender, TouchEventArgs e) => { openOSK(); };
            PreviewTouchDown += (object sender, TouchEventArgs e) =>
            {
                renderView();
            };

            FullName.MouseDown += (object sender, MouseButtonEventArgs e) => { openOSK(); };
            Password.MouseDown += (object sender, MouseButtonEventArgs e) => { openOSK(); };
            EmailAddr.MouseDown += (object sender, MouseButtonEventArgs e) => { openOSK(); };
            PhoneNumber.MouseDown += (object sender, MouseButtonEventArgs e) => { openOSK(); };
            ShowPassTxtBox.MouseDown += (object sender, MouseButtonEventArgs e) => { openOSK(); };
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
            if (!Osklib.OnScreenKeyboard.IsOpened())
                Osklib.OnScreenKeyboard.Show();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignupDetailsViewModel)DataContext).Password = ((PasswordBox)sender).Password;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Password.Visibility = Visibility.Collapsed;
            ShowPassTxtBox.Visibility = Visibility.Visible;
        }
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            Password.Visibility = Visibility.Visible;
            ShowPassTxtBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPassTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Password.Password = ShowPassTxtBox.Text;
        }

    }
}
