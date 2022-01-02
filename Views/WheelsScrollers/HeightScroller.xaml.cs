using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for HeightScroller.xaml
    /// </summary>
    public partial class HeightScroller : UserControl
    {

        public HeightScroller()
        {
            InitializeComponent();
            var ls = Enumerable.Range(130, 90);
            heights.ItemsSource = ls;
            ViewModels.SignupViewModel.OnChangeScreen += OnScrollerLoaded;
        }


        public void OnScrollerLoaded(string name) //makes sure ListBox is on sensible value when opened
        {
            if (name != "height")
                return;
            if (heights.SelectedIndex == -1)
                heights.ScrollIntoView(heights.Items.GetItemAt(45));
            else
                heights.ScrollIntoView(heights.SelectedItem);
        }

        private void heights_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListBox view = sender as ListBox;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            StrongReferenceMessenger.Default.Send(new SignupVMMessage(SignupProperty.Height, heights.SelectedItem));
        }

        private void heights_ManipulationBoundaryFeedback(object sender, System.Windows.Input.ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
