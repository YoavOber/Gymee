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
            App.Current.Dispatcher.Invoke(() =>
            {
                weights.ScrollIntoView(weights.Items[weights.Items.Count / 2]);
            });
        }

        private void heights_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListView view = sender as ListView;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            StrongReferenceMessenger.Default.Send(new SignupVMMessage(SignupProperty.Weight, weights.SelectedItem));
        }

    }
}
