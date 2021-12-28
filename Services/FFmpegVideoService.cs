using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeeDestkopApp.Services
{
    /*  JSON format is :
     *  [ //must have - indicates data is an array
            { 
                   //each item is of type FFmpegStamps 
              "InitTimeStamp": [hh:mm:ss],
              "Duration": [double],
              "VidName": [string]
            },
              ...
        ]
    */
    public struct FFmpegStamps
    {
        public string InitTimeStamp { get; set; }
        public double Duration { get; set; }
        public string VidName { get; set; }
    }

    public class FFmpegVideoService
    {
        
        const string FORMAT = "mp4";
       
        public const string CONFIG_PATH = "VidConfig.json";
        public const string Identifier = "E_";
        //identifier for edited video files

        public static List<FFmpegStamps> GetAllStamps(string path=CONFIG_PATH)
        {
            string Data = File.ReadAllText(path);
            var obj = JsonConvert.DeserializeObject(Data);
            JArray jarr = obj as JArray;
            var stamps = jarr.ToObject<List<FFmpegStamps>>();
            return stamps;
        }

        public static List<string> CutVideo(string videoPath)
        {
            List<string> outputs_ls = new List<string>();
            var stamps = GetAllStamps();
            foreach (var ts in stamps)
            {
                string output = $"{ Identifier }{ ts.VidName}.{ FORMAT}";
                outputs_ls.Add(output);
                ProcessStartInfo processStartInfo = new()
                {
                    FileName = "ffmpeg.exe",
                    Arguments = $"-ss {ts.InitTimeStamp} -i {videoPath} -t {ts.Duration}" +
                                $" -c copy {output}",
                    CreateNoWindow = true
                };
                Process.Start(processStartInfo);
            }
            return outputs_ls;
        }

    }
}
