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
using GymeeDestkopApp.Models;

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for WeightScroller.xaml
    /// </summary>
    public partial class WeightScroller : UserControl
    {
        public WeightScroller()
        {
            InitializeComponent();
            var ls = Enumerable.Range(40, 90);
            weights.ItemsSource = ls;
            ViewModels.SignupViewModel.OnChangeScreen += OnScrollerLoaded;
        }

        public void OnScrollerLoaded(string name)//makes sure ListBox is on sensible value when opened
        {
            if (name != "weight")
                return;
            if (weights.SelectedIndex == -1)
                weights.ScrollIntoView(weights.Items.GetItemAt(30));
            else
                weights.ScrollIntoView(weights.SelectedItem);
        }


        private void heights_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListBox view = sender as ListBox;
                view.ScrollIntoView(weights.SelectedItem);
            });
            StrongReferenceMessenger.Default.Send(new SignupVMMessage(SignupProperty.Weight, weights.SelectedItem));
        }

        private void weights_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
