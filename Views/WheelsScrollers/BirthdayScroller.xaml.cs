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
    /// Interaction logic for BirthdayScroller.xaml
    /// </summary>
    public partial class BirthdayScroller : UserControl
    {
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;
        public BirthdayScroller()
        {
            InitializeComponent();
            List<int> days = Enumerable.Range(1, 31).ToList();

            Days.ItemsSource = days;
            var ls = new List<string>()
            {
                "ספטמבר","אוקטובר","נובמבר","דצמבר","ינואר","פברואר","מרץ",
                "אפריל","מאי","יוני","יולי","אוגוסט"
            };
            Month.ItemsSource = ls;

            List<int> years = Enumerable.Range(1940, 80).ToList();
            year.ItemsSource = years;

            App.Current.Dispatcher.Invoke(() =>
            {
                Days.ScrollIntoView(Days.Items[Days.Items.Count / 2]);
                year.ScrollIntoView(year.Items[year.Items.Count / 2]);
                Month.ScrollIntoView(Month.Items[Month.Items.Count / 2]);
            });
        }

        public void Reset()
        {

        }
        private void day_SelectionChanged(object sender, SelectionChangedEventArgs e) //naming is bad due to copy-paste
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListView view = sender as ListView;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            Messenger.Send(new SignupVMMessage(SignupProperty.BdDay, Days.SelectedItem));
        }

        private void month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListView view = sender as ListView;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            Messenger.Send(new SignupVMMessage(SignupProperty.BdMonth, Month.SelectedIndex));
        }

        private void year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListView view = sender as ListView;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            Messenger.Send(new SignupVMMessage(SignupProperty.BdYear, year.SelectedItem));
        }
    }
}
