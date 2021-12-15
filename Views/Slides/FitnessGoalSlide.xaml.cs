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
    /// Interaction logic for FitnessGoalSlide.xaml
    /// </summary>
    public partial class FitnessGoalSlide : UserControl
    {
        private readonly BrushConverter converter = new BrushConverter();
        private StrongReferenceMessenger Messanger { get; set; } = StrongReferenceMessenger.Default;
        public FitnessGoalSlide()
        {
            InitializeComponent();

            Muscle.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Weight.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Overall.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            MuscleTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            WeightTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            OverallTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
        }

        private void Begginer_Click(object sender, RoutedEventArgs e)
        {
            Muscle.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            Weight.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Overall.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            MuscleTxt.Foreground = Brushes.White;
            WeightTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            OverallTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            Messanger.Send(new SignupVMMessage(SignupProperty.Goal, Goal.INCREASE_MUSCLE));
            // FitnessExperienceViewModel.FitnessLevel = FitnessLevel.BEGINNER;
        }

        private void Intermediate_Click(object sender, RoutedEventArgs e)
        {
            Muscle.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Weight.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            Overall.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            MuscleTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            WeightTxt.Foreground = Brushes.White;
            OverallTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            Messanger.Send(new SignupVMMessage(SignupProperty.Goal, Goal.TONE_MUSCLE));

            // FitnessExperienceViewModel.FitnessLevel = FitnessLevel.INTERMEDIATE;
        }

        private void Advanced_Click(object sender, RoutedEventArgs e)
        {
            Muscle.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Weight.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Overall.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            WeightTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            MuscleTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            OverallTxt.Foreground = Brushes.White;
            Messanger.Send(new SignupVMMessage(SignupProperty.Goal, Goal.GENERAL));
            //   FitnessExperienceViewModel.FitnessLevel = FitnessLevel.ADVANCED;
        }
    }
}
