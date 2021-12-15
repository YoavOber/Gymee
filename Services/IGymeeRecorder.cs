using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymeeDestkopApp.Services
{
    public enum RecordingState
    {
        BEFORE,
        RECORDING,
        DONE
    }

    public interface IGymeeRecorder 
    {
        RecordingState GetRecordingState();
        string GetDepthFramesPath();
        string GetVideoPath();
        bool IsProcessing();
        void Start(string recordId);
        void End();
    }
}
