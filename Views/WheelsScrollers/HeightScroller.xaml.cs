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
                heights.ScrollIntoView(heights.Items[heights.Items.Count / 2]);
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
