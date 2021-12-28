using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GymeeDestkopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer Timer { get; set; }
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;

        public MainWindow()
        {
            InitializeComponent();
            Timer = new Timer(1000 * 90);
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
            MainHost.PreviewTouchMove += ResetTimerFingerTouch;
            MainHost.PreviewMouseMove += ResetTimerMouseTouch;
            Messenger.Register<MainWindow, ChangePageMessage>(this, (r, m) => r.Receive(m));
        }

        private void ResetTimerMouseTouch(object sender, MouseEventArgs e) 
        {
            Timer.Stop();
            Timer.Start();
        }

        private void ResetTimerFingerTouch(object sender, TouchEventArgs e)
        {
            Timer.Stop();
            Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
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
                    Timer.Start();
                    Osklib.OnScreenKeyboard.Close();
                    break;

                case PageIndex.WORKOUT_VIDEO:
                    Timer.Stop();
                    break;

                default:
                    Timer.Stop();
                    Timer.Start();
                    break;
            }

            MainHost.SelectedIndex = index;
        }
          /*if (message.Index == PageIndex.INTRO_PAGE)
            {
                Messenger.Send("resetVM");
                Reset();
                Timer.Start();
                Osklib.OnScreenKeyboard.Close();
                RandomSoundPlayer.PlayShuffle();
            }
            else if (message.Index == PageIndex.WORKOUT_VIDEO)
            {
                Timer.Stop();
            }
            else if (message.Index == PageIndex.POST_WORKOUT_VIEW)
            {
                Timer.Start();
            }*/

        private void ResetAllViews()
        {
            FitnessDataView.Reset();
            LoginDataView = new GymeeDesktopApp.Views.LoginView();
            SignupDataView = new GymeeDesktopApp.Views.SignupDataView();
        }
    }
}
