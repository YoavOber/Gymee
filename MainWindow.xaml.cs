using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace GymeeDestkopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer NoTouchTimer { get; set; }
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;

        public MainWindow()
        {
            InitializeComponent();
            //screen timer
            Loaded += (object sender, RoutedEventArgs e) =>
            {
                KioskModeHelper.BeginKioskMode();
            };
            NoTouchTimer = new Timer(1000 * 90);//add configuration file
            NoTouchTimer.Elapsed += Timer_Elapsed;
            NoTouchTimer.Start();
            MainHost.PreviewTouchMove += ResetTimerFingerTouch;
            MainHost.PreviewMouseMove += ResetTimerMouseTouch;
            Messenger.Register<MainWindow, ChangePageMessage>(this, (r, m) => r.Receive(m));
        }

        private void ResetTimerMouseTouch(object sender, MouseEventArgs e)
        {
            if (MainHost.SelectedIndex == (int)PageIndex.WORKOUT_VIDEO)
                return;
            NoTouchTimer.Stop();
            NoTouchTimer.Start();
        }

        private void ResetTimerFingerTouch(object sender, TouchEventArgs e)
        {
            if (MainHost.SelectedIndex == (int)PageIndex.WORKOUT_VIDEO)
                return;
            NoTouchTimer.Stop();
            NoTouchTimer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (MainHost.SelectedIndex == (int)PageIndex.WORKOUT_VIDEO)
                    return;
                MainHost.SelectedIndex = 0;
                ResetAllViews();
                Messenger.Send("resetVM");
                Osklib.OnScreenKeyboard.Close();
            });
        }

        ~MainWindow()
        {
            Messenger.UnregisterAll(this);
        }

        private void Receive(ChangePageMessage message)
        {
            int index = (int)message.Index;
            if (MainHost.SelectedIndex == index)
                return;

            switch (message.Index)
            {
                case PageIndex.INTRO_PAGE:
                    Messenger.Send("resetVM");
                    ResetAllViews();
                    NoTouchTimer.Start();
                    Osklib.OnScreenKeyboard.Close();
                    break;

                case PageIndex.WORKOUT_VIDEO:
                    NoTouchTimer.Stop();
                    break;

                default:
                    NoTouchTimer.Stop();
                    NoTouchTimer.Start();
                    break;
            }

            MainHost.SelectedIndex = index;
        }

        private void ResetAllViews()
        {
            FitnessDataView.Reset();
            LoginDataView = new GymeeDesktopApp.Views.LoginView();
            SignupDataView = new GymeeDesktopApp.Views.SignupDataView();
        }
    }
}
