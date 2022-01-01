using System;
using System.Collections.Generic;
using System.Drawing;
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
using Intel.RealSense;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Timers;
using System.IO;
using GymeeDestkopApp.Models;

namespace GymeeDestkopApp.Services
{

    public class RealSenseGymeeRecorder : IGymeeRecorder
    {
        RecordingState recording = RecordingState.BEFORE;
        Pipeline pipeline;
        static string rootFolder = ConfigurationService.GetConfiguration().RecordFolder;
        bool processing = false;
        string pngDirectory = $"{rootFolder}/pngs";
        string depthDirectory = $"{rootFolder}/depths";
        string videosDirectory = $"{rootFolder}/videos";
        int fps;
        string pngPrefix = "rgb";
        string recordId;
        int pngCount = 1;
        FrameQueue queue;
        Config cfg;

        public RealSenseGymeeRecorder(int width = 1280, int height = 720, int fps = 30)
        {
            Directory.CreateDirectory(this.pngDirectory);
            Directory.CreateDirectory(this.depthDirectory);
            Directory.CreateDirectory(this.videosDirectory);
            this.fps = fps;
            this.cfg = new Config();
            //don't change the format or this will crash :)
            cfg.EnableStream(Intel.RealSense.Stream.Depth, width, height, Format.Z16, fps);
            cfg.EnableStream(Intel.RealSense.Stream.Color, width, height, Format.Rgb8, fps);
            this.pipeline = new Pipeline();
            this.queue = new FrameQueue();

        }

        public RecordingState GetRecordingState()
        {
            return this.recording;
        }

        public bool IsProcessing()
        {
            return this.processing;
        }

        public string GetDepthFramesPath()
        {
            var depthFramesPath = $"{this.depthDirectory}/{this.recordId}";
            if (Directory.Exists(depthFramesPath))
            {
                return depthFramesPath;
            }
            return null;
        }

        public string GetVideoPath()
        {
            var videoPath = $"{this.videosDirectory}/{this.recordId}.mp4";
            if (File.Exists(videoPath))
            {
                return videoPath;
            }
            return null;
        }

        public string GetVideoFilePath()
        {
            return this.videosDirectory;
        }
        public void Start(string recordId)
        {
            if (this.recording != RecordingState.BEFORE)
            {
                return;
            }

            this.recording = RecordingState.RECORDING;
            this.recordId = recordId;
            Directory.CreateDirectory($"{this.depthDirectory}/{this.recordId}");
            Directory.CreateDirectory($"{this.pngDirectory}/{this.recordId}");

            this.pipeline.Start(cfg);
            Task.Run(() =>
            {
                using (var process = Process.GetCurrentProcess())
                {
                    // only run on core number 1
                    process.ProcessorAffinity = (IntPtr)0x0001;
                }
                while (true)
                {
                    FrameSet frames;
                    if (queue.PollForFrame(out frames))
                    {
                        using (frames)
                        using (var color = frames.ColorFrame)
                        using (var depth = frames.DepthFrame)
                        //we save the least processed color and depth outputs and store them for post record processing
                        {
                            using var bitmap = GymeeTransforms.GenerateRGBBitmap(color);
                            bitmap.Save($"{this.pngDirectory}/{this.recordId}/{this.pngPrefix}{this.pngCount}.png");
                            GymeeTransforms.WriteDepthFrameToFile(depth, $"{this.depthDirectory}/{this.recordId}/depth{this.pngCount}");
                            this.pngCount++;
                        }
                    }
                }
            });
            //run recording in another thread which is dependent on the recording state
            Task.Run(() =>
            {
                while (this.recording == RecordingState.RECORDING)
                {
                    using (var frames = pipeline.WaitForFrames())
                    {
                        Console.WriteLine("queueing");
                        queue.Enqueue(frames);
                    }
                }
            });
        }


        public void End()
        {
            if (this.recording != RecordingState.RECORDING)
            {
                return;
            }
            this.recording = RecordingState.DONE;
            this.pipeline.Stop();
            Task.Run(() =>
            {
                this.processing = true;
                var pngFiles = Directory.GetFiles($"{this.pngDirectory}/{this.recordId}", "*.png");
                foreach (var pngFileName in pngFiles)
                {
                    using (var bitmap = new Bitmap(pngFileName))
                    {
                        GymeeTransforms.FixRealSenseBitmap(bitmap);
                        bitmap.Save(pngFileName);
                    }
                }
                this.pngCount = 1;
                ProcessStartInfo processStartInfo = new()
                {
                    FileName = "ffmpeg.exe",
                    Arguments = $"-framerate {this.fps} -i {this.pngDirectory}/{this.recordId}/{this.pngPrefix}%d.png " +
                    $"-c:v libx264 -pix_fmt yuv420p -crf 25 {this.videosDirectory }/{this.recordId}.mp4",
                    CreateNoWindow = true
                };
                var p = Process.Start(processStartInfo);
                this.processing = false;
                this.recording = RecordingState.BEFORE;
                string ndpPath = GetDepthFramesPath(); ;
                EditFileNames(ndpPath); // use GetDepthFramesPath(); ?
                p.WaitForExit();
                FFmpegVideoService.CutVideo(this.recordId,"mp4",this.videosDirectory);
            });
        }

        public void EditFileNames(string path)
        {
            char mark = '$';
            var stamps = FFmpegVideoService.GetAllStamps();
            foreach (var st in stamps)
            {
                TimeSpan timeSpan = TimeSpan.Parse(st.InitTimeStamp);
                long start = (long)timeSpan.TotalSeconds * this.fps + 1;//this is the first file - +1 since pngCount is initialized to 1
                long end = (long)(start + st.Duration * this.fps);
                int fileNameIndex = 1;
                for (var i = start; i <= end; i++, fileNameIndex++)
                {
                    File.Move(@$"{path}\depth{i}.ndp",
                              @$"{path}\{st.VidName}_{fileNameIndex}{mark}.ndp");//rename
                }
            }
            DirectoryInfo d = new DirectoryInfo(path);
            foreach (var f in d.GetFiles())
                if (!f.Name.Contains(mark) && f.Name.EndsWith("ndp"))
                    f.Delete();
        }
    }
}
