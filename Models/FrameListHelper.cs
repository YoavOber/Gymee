using GymeeDestkopApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeeDestkopApp.Models
{
    public class FrameListHelper : IComparer<Tuple<long, long>>
    {

        public int Compare(Tuple<long, long> x, Tuple<long, long> y)//compare function
        {
            if (x.Item1 > y.Item1)
                return 1;
            if (x.Item2 < y.Item1)
                return -1;
            return 0;
        }

        public static List<Tuple<long, long>> GetCropRanges(int fps)
        {
            var stamps = FFmpegVideoService.GetAllStamps();
            var converter = new Converter<FFmpegStamps, Tuple<long, long>>(st =>
            {
                TimeSpan timeSpan = TimeSpan.Parse(st.InitTimeStamp);

                long start = timeSpan.Ticks + 1;//this is the first file - +1 since pngCount is initialized to 1
                long end = (long)(start + st.Duration * fps);
                return new Tuple<long, long>(start, end);
            });
            var result = stamps.ConvertAll(converter);
            return result;
        }

    }
}
