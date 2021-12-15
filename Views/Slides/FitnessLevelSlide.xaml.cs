using GymeeDesktopApp.Models;
using GymeeDestkopApp.Models;
using GymeeDestkopApp.ViewModels;
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
    /// Interaction logic for FitnessLevelSlide.xaml
    /// </summary>
    public partial class FitnessLevelSlide : UserControl
    {
        private readonly BrushConverter converter = new BrushConverter();
        private StrongReferenceMessenger Messanger { get; set; } = StrongReferenceMessenger.Default;
        public FitnessLevelSlide()
        {
            InitializeComponent();
            //Begginer.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            //Intermediate.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            //Advanced.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            //BegginerTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            //IntermTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            //AdvancedTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            
        }

        private void Begginer_Click(object sender, RoutedEventArgs e)
        {
            Begginer.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            Intermediate.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Advanced.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            BegginerTxt.Foreground = Brushes.White;
            IntermTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            AdvancedTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");

            Messanger.Send(new SignupVMMessage(SignupProperty.FitnessLevel, FitnessLevel.BEGINNER));
            // FitnessExperienceViewModel.FitnessLevel = FitnessLevel.BEGINNER;
        }

        private void Intermediate_Click(object sender, RoutedEventArgs e)
        {
            Begginer.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Intermediate.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            Advanced.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            BegginerTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            IntermTxt.Foreground = Brushes.White;
            AdvancedTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");

            Messanger.Send(new SignupVMMessage(SignupProperty.FitnessLevel, FitnessLevel.INTERMEDIATE));

            // FitnessExperienceViewModel.FitnessLevel = FitnessLevel.INTERMEDIATE;
        }

        private void Advanced_Click(object sender, RoutedEventArgs e)
        {
            Begginer.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Intermediate.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.OffBtnHex);
            Advanced.Background = (SolidColorBrush)converter.ConvertFromString(BoolToFillConverter.TrueValueHex);
            BegginerTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            IntermTxt.Foreground = (SolidColorBrush)converter.ConvertFromString("#FF959595");
            AdvancedTxt.Foreground = Brushes.White;

            Messanger.Send(new SignupVMMessage(SignupProperty.FitnessLevel, FitnessLevel.ADVANCED));
            //   FitnessExperienceViewModel.FitnessLevel = FitnessLevel.ADVANCED;
        }

    }
}
