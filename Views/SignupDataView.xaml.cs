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
            FullName.PreviewTouchDown += (object sender, TouchEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            Password.PreviewTouchDown += (object sender, TouchEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            EmailAddr.PreviewTouchDown += (object sender, TouchEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            PhoneNumber.PreviewTouchDown += (object sender, TouchEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            PreviewTouchDown += (object sender, TouchEventArgs e) => { renderView(); };

            FullName.MouseDown += (object sender, MouseButtonEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            Password.MouseDown += (object sender, MouseButtonEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            EmailAddr.MouseDown += (object sender, MouseButtonEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            PhoneNumber.MouseDown += (object sender, MouseButtonEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            ShowPassTxtBox.MouseDown += (object sender, MouseButtonEventArgs e) => { Osklib.OnScreenKeyboard.Show(); };
            MouseDown += (object sender, MouseButtonEventArgs e) => { renderView(); };
        }

        private void renderView()
        {
            Osklib.OnScreenKeyboard.Close();
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
