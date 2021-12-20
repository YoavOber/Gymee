using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Linq;
using System.Windows.Controls;

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
            App.Current.Dispatcher.Invoke(() =>
            {
                heights.ScrollIntoView(190);
            });
        }


        private void heights_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListView view = sender as ListView;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            StrongReferenceMessenger.Default.Send(new SignupVMMessage(SignupProperty.Height, heights.SelectedItem));
        }

    }
}
