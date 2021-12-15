using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for TermsOfUse.xaml
    /// </summary>
    public partial class TermsOfUse : UserControl
    {
        const string HebrewPath = "HebTerms.txt";
        public TermsOfUse()
        {
            InitializeComponent();
            if (File.Exists(HebrewPath))
                TermsOfUseBlock.Text = File.ReadAllText(HebrewPath);
            else
                TermsOfUseBlock.Text = "Error - Can't find terms.txt";
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.SIGNUP_DATA));
            checkbox.IsChecked = false;
            checkbox_Unchecked(null, null);
        }


        private readonly BrushConverter converter = new BrushConverter();
        private void checkbox_Checked(object sender, RoutedEventArgs e)
        {
            NextBtn.Background = (Brush)converter.ConvertFromString(GymeeDesktopApp.Models.BoolToFillConverter.TrueValueHex);
            NextTxt.Foreground = Brushes.White;
        }

        private void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            NextBtn.Background = (Brush)converter.ConvertFromString(GymeeDesktopApp.Models.BoolToFillConverter.OffBtnHex);
            NextTxt.Foreground = (Brush)converter.ConvertFromString("#FF999999");
        }

        private void TermsOfUseBlock_TouchDown(object sender, TouchEventArgs e)
        {
            Osklib.OnScreenKeyboard.Close();
        }

        private void TermsOfUseBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            Osklib.OnScreenKeyboard.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.LOGIN));
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.START_SIGNUP_OR_LOGIN));
        }
    }
}
