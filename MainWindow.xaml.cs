using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
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
            if (MainHost.SelectedIndex == (int)PageIndex.WORKOUT_VIDEO)
                return;
            Timer.Stop();
            Timer.Start();
        }

        private void ResetTimerFingerTouch(object sender, TouchEventArgs e)
        {
            if (MainHost.SelectedIndex == (int)PageIndex.WORKOUT_VIDEO)
                return;
            Timer.Stop();
            Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (MainHost.SelectedIndex == (int)PageIndex.WORKOUT_VIDEO)
                return;
            MainHost.SelectedIndex = 0;
            ResetAllViews();
            Messenger.Send("resetVM");
            Osklib.OnScreenKeyboard.Close();
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

        private void ResetAllViews()
        {
            FitnessDataView.Reset();
            LoginDataView = new GymeeDesktopApp.Views.LoginView();
            SignupDataView = new GymeeDesktopApp.Views.SignupDataView();
        }
    }
}
