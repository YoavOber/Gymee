﻿using GymeeDestkopApp.Models;
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
        int startYear = 1920;
        int maxAge = 100;
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;
        public BirthdayScroller()
        {
            InitializeComponent();
            List<int> days = Enumerable.Range(1, 31).ToList();

            Days.ItemsSource = days;
            var ls = new List<string>()
            {
                "ינואר","פברואר","מרץ", "אפריל","מאי","יוני","יולי",
                "אוגוסט","ספטמבר","אוקטובר","נובמבר","דצמבר"
            };
            Month.ItemsSource = ls;

            List<int> years = Enumerable.Range(startYear, maxAge).ToList();
            Years.ItemsSource = years;

            ViewModels.SignupViewModel.OnChangeScreen += OnScrollerLoaded;
        }

        private void OnScrollerLoaded(string name)//makes sure ListBox is on sensible value when opened
        {
            if (name != "age")
                return;
            if (Years.SelectedIndex == -1)
                Years.ScrollIntoView(Years.Items.GetItemAt(2000 - startYear));//year 2000 ~
            else
            {
                Years.ScrollIntoView(Years.SelectedItem);
                Month.ScrollIntoView(Month.SelectedItem);
                Days.ScrollIntoView(Days.SelectedItem);
            }
        }

        private void day_SelectionChanged(object sender, SelectionChangedEventArgs e) //naming is bad due to copy-paste
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListBox view = sender as ListBox;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            Messenger.Send(new SignupVMMessage(SignupProperty.BdDay, Days.SelectedItem));
        }

        private void month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListBox view = sender as ListBox;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            Messenger.Send(new SignupVMMessage(SignupProperty.BdMonth, Month.SelectedIndex));
        }

        private void year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                ListBox view = sender as ListBox;
                view.ScrollIntoView(e.AddedItems[0]);
            });
            Messenger.Send(new SignupVMMessage(SignupProperty.BdYear, Years.SelectedItem));
        }

        private void Month_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void Days_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void Years_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

    }
}
