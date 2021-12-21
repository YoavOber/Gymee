using GymeeDestkopApp.ViewModels;
using MaterialDesignThemes.Wpf.Transitions;
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
    /// Interaction logic for FitnessLevelView.xaml
    /// </summary>
    public partial class FitnessDataView : UserControl
    {
        public FitnessDataView()
        {
            InitializeComponent();
            Transitioner.MouseDown += (object sender, MouseButtonEventArgs e) =>
            {
                WheelSelector.SelectedIndex = -1;
            };
            Transitioner.PreviewTouchDown += (object sender, TouchEventArgs e) =>
            {
                WheelSelector.SelectedIndex = -1;
            };

        }
        public void Reset()
        {
            LevelSlide.Content =  new FitnessLevelSlide();

            GoalSlide.Content = new FitnessGoalSlide();

            FreqSlide.Content = new WorkoutFrequencySlide();
            MeasureSlide.Content = new MeasurementsSlide();

            BDayScroller.Content = new BirthdayScroller();
            GenderScroller.Content = new GenderScroller();
            HeightScroller.Content = new HeightScroller();
            WeightScroller.Content = new WeightScroller();
        }

    }
}
