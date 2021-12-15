using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GymeeDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for Explaination_1.xaml
    /// </summary>
    public partial class Explaination_1 : UserControl
    {
        //TODO - reduce code behind as mush as possible
        private Ellipse[] ProgressBtns { get; set; } //progress buttons 
        public Explaination_1()
        {
            InitializeComponent();
            ProgressBtns = new Ellipse[] { PrgrsBtn0, PrgrsBtn1, PrgrsBtn2, PrgrsBtn3 };
        }

        private void MoveNext_Click(object sender, RoutedEventArgs e)
        {
            if (ExplainationSequence.SelectedIndex < ExplainationSequence.Items.Count - 1)
                ExplainationSequence.SelectedIndex++;
            MovePrev.Visibility = ExplainationSequence.SelectedIndex != 0 ? Visibility.Visible : Visibility.Collapsed;
            MoveNext.Visibility = ExplainationSequence.SelectedIndex != ExplainationSequence.Items.Count - 1 ? Visibility.Visible : Visibility.Collapsed;
            for (int i = 0; i < ProgressBtns.Length; i++)
            {
                ProgressBtns[i].Fill = i == ExplainationSequence.SelectedIndex ? Brushes.White : Brushes.Black;
            }
        }

        private void MovePrev_Click(object sender, RoutedEventArgs e)
        {
            if (ExplainationSequence.SelectedIndex > 0)
                ExplainationSequence.SelectedIndex--;
            MovePrev.Visibility = ExplainationSequence.SelectedIndex != 0 ? Visibility.Visible : Visibility.Collapsed;
            MoveNext.Visibility = ExplainationSequence.SelectedIndex != ExplainationSequence.Items.Count - 1 ? Visibility.Visible : Visibility.Collapsed;
            for (int i = 0; i < ProgressBtns.Length; i++)
            {
                ProgressBtns[i].Fill = i == ExplainationSequence.SelectedIndex ? Brushes.White : Brushes.Black;
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            ExplainationSequence.SelectedIndex = 0;
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.START_SIGNUP_OR_LOGIN));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ExplainationSequence.SelectedIndex = 0;
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.LOGIN));
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.INTRO_PAGE));
        }

        //private void BackBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    ExplainationSequence.SelectedIndex = 0;
        //    StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.INTRO_PAGE));
        //}


    }
}
