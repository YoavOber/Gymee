using GymeeDesktopApp.Models;
using GymeeDestkopApp.Models;
using GymeeDestkopApp.Services;
using MaterialDesignThemes.Wpf;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for WorkoutVideoView.xaml
    /// </summary>
    public partial class WorkoutVideoView : UserControl
    {
        //video filenames by trainee's experience
        const string BEGGINER_VID = "Workout Video.mov";
        const string INTERMEDIATE_VID = "Workout Video.mov";
        const string ADVENCED_VID = "Workout Video.mov";

        private IGymeeRecorder GymeeRecorder { get; set; }
        private MediaPlayer SoundPlayer { get; set; }
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;

        private GymeeAuthenticateService.LoginResult userData { get; set; }

        public WorkoutVideoView()
        {
            InitializeComponent();
            QuitDialogBox.Visibility = Visibility.Collapsed;
            var config = ConfigurationService.GetConfiguration();
            GymeeRecorder = new RealSenseGymeeRecorder(config.Width, config.Height, config.Fps);
            SoundPlayer = new MediaPlayer();
            Messenger.Register<WorkoutVideoView, ChangePageMessage>(this, (r, m) =>
             {
                 if (m.Index == PageIndex.PRE_WORKOUT)
                 {
                     var data = (GymeeAuthenticateService.LoginResult)m.Data;
                     userData = new GymeeAuthenticateService.LoginResult
                     {
                         email = data.email,
                         name = data.name,
                         fitnessLevel = data.fitnessLevel
                     };
                 }
                 else if (m.Index == PageIndex.WORKOUT_VIDEO)
                 {
                     Start();
                 }
             });
        }

        //as Tamir and Omri asked - video source uri is loaded according to level
        private void Start()
        {
            string uri = "pack://siteoforigin:,,/Views/Media/";
            switch (userData.fitnessLevel)
            {
                case FitnessLevel.Beginner:
                    uri += BEGGINER_VID;
                    break;
                case FitnessLevel.Intermediate:
                    uri += INTERMEDIATE_VID;
                    break;
                case FitnessLevel.Advanced:
                    uri += ADVENCED_VID;
                    break;
                default:
                    uri += BEGGINER_VID;
                    break;
            }
            VideoPlayer.Source = new Uri(uri);
            VideoPlayer.Position = TimeSpan.Zero;
            VideoPlayer.Loaded += Video_Loaded;
            if (VideoPlayer.IsLoaded)
                Video_Loaded(null, null);
        }

        private void Video_Loaded(object sender, RoutedEventArgs e) //used to synchronize sound and picture
        {
            GymeeRecorder.Start(userData.email);
            VideoPlayer.Play();
            PlayRandomTrack();
        }

        private void PlayRandomTrack()
        {
            string[] soundList = Directory.GetFiles("Views\\Media\\Sounds");
            string fname = soundList[new Random().Next(soundList.Length - 1)];
            SoundPlayer.Open(new Uri(Directory.GetCurrentDirectory() + '/' + fname));
            SoundPlayer.Play();
        }

        private async void Video_MediaEnded(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Stop();
            GymeeRecorder.End();
            var result = await GymeeAuthenticateService.onAssessmentDone(userData.email, userData.name);
            if (result)
            {
                VideoPlayer.Loaded -= Video_Loaded;
                TerminateWorkout(false);
            }
            else
            {
                //handle ? Never reaches here!
            }
        }

        private async void exitButton_Click(object sender, RoutedEventArgs e)
        {
            QuitDialogBox.Visibility = Visibility.Visible;
            VideoPlayer.Pause();
            SoundPlayer.Pause();
            bool result = (bool)await QuitDialogBox.ShowDialog(QuitDialogBox.Content);
            if (result)
                TerminateWorkout(true);
            else
            {
                VideoPlayer.Play();
                SoundPlayer.Play();
            }
        }

        private void TerminateWorkout(bool workoutTerminated)
        //workoutTerminated - was workout terminated manually by user or ended due to video ended
        {
            VideoPlayer.Stop();
            SoundPlayer.Stop();
            Messenger.Send(new ChangePageMessage(workoutTerminated ? PageIndex.INTRO_PAGE : PageIndex.POST_WORKOUT_VIEW));
            //reset icon and unmute
            PackIcon icon = muteBtn.Content as PackIcon;
            icon.Kind = PackIconKind.VolumeHigh;
            SoundPlayer.IsMuted = false;
            QuitDialogBox.Visibility = Visibility.Collapsed;
        }

        private void muteBtnClick(object sender, RoutedEventArgs e)
        {
            Button muteBtn = sender as Button;
            PackIcon icon = muteBtn.Content as PackIcon;

            bool isMuted = icon.Kind == PackIconKind.VolumeOff;
            icon.Kind = isMuted ? PackIconKind.VolumeHigh : PackIconKind.VolumeOff;

            SoundPlayer.IsMuted = !isMuted;
        }

    }
}
