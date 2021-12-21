using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Windows;
using System.Windows.Controls;
using GymeeDestkopApp.Services;
using GymeeDesktopApp.Models;
using MaterialDesignThemes.Wpf;
using NAudio.CoreAudioApi;
using System.Media;
using System.IO;
using System.Windows.Media;

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for WorkoutVideoView.xaml
    /// </summary>
    public partial class WorkoutVideoView : UserControl
    {
        //video filenames by user experience
        const string BEGGINER_VID = "Workout Video.mov";
        const string INTERMEDIATE_VID = "Workout Video.mov";
        const string ADVENCED_VID = "Workout Video.mov";

        private IGymeeRecorder GymeeRecorder { get; set; }
        private MediaPlayer SoundPlayer { get; set; }
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;

        public static FitnessLevel Level { get; set; } 

        public WorkoutVideoView()
        {
            InitializeComponent();
            QuitDialogBox.Visibility = Visibility.Collapsed;
            //GymeeRecorder = new RealSenseGymeeRecorder();
            SoundPlayer = new MediaPlayer();
            Messenger.Register<WorkoutVideoView, ChangePageMessage>(this, (r, m) =>
             {
                 if (m.Index == PageIndex.WORKOUT_VIDEO)
                     Start();
             });
        }

        //as Tamir and Omri asked - video source uri is loaded according to level
        private void Start()
        {
            //TODO: Yoav insert some record id here based on user
            //Yoav : meanwhile lets use random id
            // var id = Guid.NewGuid().ToString();
            //GymeeRecorder.Start(id);
            string uri = "pack://siteoforigin:,,/Views/Media/";
            switch (Level)
            {
                case FitnessLevel.BEGINNER:
                    uri += BEGGINER_VID;
                    break;
                case FitnessLevel.INTERMEDIATE:
                    uri += INTERMEDIATE_VID;
                    break;
                case FitnessLevel.ADVANCED:
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

        private void Video_MediaEnded(object sender, RoutedEventArgs e)
        {
            //GymeeRecorder.End();
            //var videoPath = GymeeRecorder.GetVideoPath();
            //   UploadVideo(vid_path)
            //  var uploader = new GoogleDriveUploader(videoPath);
            //   uploader.Upload
            SoundPlayer.Stop();
            Messenger.Send(new ChangePageMessage(PageIndex.POST_WORKOUT_VIEW));
            VideoPlayer.Loaded -= Video_Loaded;
        }

        private async void exitButton_Click(object sender, RoutedEventArgs e)
        {
            QuitDialogBox.Visibility = Visibility.Visible;
            VideoPlayer.Pause();
            SoundPlayer.Pause();
            bool result = (bool)await QuitDialogBox.ShowDialog(QuitDialogBox.Content);
            if (result)
                TerminateWorkout();
            else
            {
                VideoPlayer.Play();
                SoundPlayer.Play();
            }
        }

        private void TerminateWorkout()
        {
            VideoPlayer.Stop();
            SoundPlayer.Stop();
            Messenger.Send(new ChangePageMessage(PageIndex.INTRO_PAGE));
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
