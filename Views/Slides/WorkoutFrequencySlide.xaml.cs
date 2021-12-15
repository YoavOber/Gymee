using GymeeDesktopApp.Models;
using GymeeDestkopApp.Models;
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

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for WorkoutFrequencySlide.xaml
    /// </summary>
    public partial class WorkoutFrequencySlide : UserControl
    {
        private readonly BrushConverter converter = new BrushConverter();
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;
        public WorkoutFrequencySlide()
        {
            InitializeComponent();
            OneOrTwo.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            ThreeOrFour.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            FiveOrMore.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            OneTwoTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            ThreeFourTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            FivePTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
        }

        private void Begginer_Click(object sender, RoutedEventArgs e)
        {
            OneOrTwo.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            ThreeOrFour.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            FiveOrMore.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            OneTwoTxt.Foreground = Brushes.White;
            ThreeFourTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            FivePTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");

            Messenger.Send(new SignupVMMessage(SignupProperty.WorkoutDays, WeeklyWorkouts.ONE_OR_TWO));
        }

        private void Intermediate_Click(object sender, RoutedEventArgs e)
        {
            OneOrTwo.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            ThreeOrFour.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            FiveOrMore.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            OneTwoTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            ThreeFourTxt.Foreground = Brushes.White;
            FivePTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");

            Messenger.Send(new SignupVMMessage(SignupProperty.WorkoutDays, WeeklyWorkouts.THREE_OR_FOUR));
        }

        private void Advanced_Click(object sender, RoutedEventArgs e)
        {
            OneOrTwo.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            ThreeOrFour.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            FiveOrMore.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            OneTwoTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            ThreeFourTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            FivePTxt.Foreground = Brushes.White;

            Messenger.Send(new SignupVMMessage(SignupProperty.WorkoutDays, WeeklyWorkouts.FIVE_OR_MORE));
        }


    }
}
