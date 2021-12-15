using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using NAudio.Wave;
using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace GymeeDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for IntroPage.xaml
    /// </summary>
    public partial class IntroPage : UserControl
    {
        public IntroPage()
        {
            InitializeComponent();
           
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.LOGIN));
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.WHAT_IS_GYMEE));
        }
    }
}
