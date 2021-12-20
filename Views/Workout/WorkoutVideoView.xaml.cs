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
        public static FitnessLevel Level { get; set; }
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;
        public WorkoutVideoView()
        {
            InitializeComponent();
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
            Video.Source = new Uri(uri);
            Video.Position = TimeSpan.Zero;
            Video.Loaded += Video_Loaded;
            if (Video.IsLoaded)
                Video_Loaded(null, null);
        }

        private void Video_Loaded(object sender, RoutedEventArgs e) //used to synchronize sound and picture
        {
            Video.Play();
            PlayRandomTrack();
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
            Video.Loaded -= Video_Loaded;
        }

        private async void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Video.Pause();
            SoundPlayer.Pause();
            bool result = (bool)await QuitDialogBox.ShowDialog(QuitDialogBox.Content);
            if (result)
                Terminate();
            else
            {
                Video.Play();
                SoundPlayer.Play();
            }
        }

        private void Terminate()
        {
            Video.Stop();
            SoundPlayer.Stop();
            Messenger.Send(new ChangePageMessage(PageIndex.INTRO_PAGE));
            muteBtn.Content = new PackIcon
            {
                Kind = PackIconKind.VolumeHigh
            };
            //UnMuteVideo();
        }

        private void muteBtnClick(object sender, RoutedEventArgs e)
        {
            Button muteBtn = sender as Button;
            PackIcon icon = muteBtn.Content as PackIcon;

            bool isMuted = icon.Kind == PackIconKind.VolumeOff;
            icon.Kind = isMuted ? PackIconKind.VolumeHigh : PackIconKind.VolumeOff;

            SoundPlayer.IsMuted = !isMuted;
            //  Video.IsMuted = !isMuted; //unmute if muted, nute uf unmuted
        }

        private void PlayRandomTrack()
        {
            //string baseUri = ;
            string[] soundList = Directory.GetFiles("Views\\Media\\Sounds");
            string fname = soundList[new Random().Next(soundList.Length - 1)];
            SoundPlayer.Open(new Uri(Directory.GetCurrentDirectory()+ '/' + fname));
            SoundPlayer.Play();
        }

        //private void MuteVideo()
        //{
        //    using (var enumerator = new MMDeviceEnumerator())
        //    {
        //        foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
        //        {
        //            device.AudioEndpointVolume.Mute = true;
        //        }
        //    }
        //}

        //private void UnMuteVideo()
        //{
        //    using (var enumerator = new MMDeviceEnumerator())
        //    {
        //        foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
        //        {
        //            device.AudioEndpointVolume.Mute = false;
        //        }
        //    }
        //}
    }
}
