using System.Threading.Tasks;

namespace VoiceCommand.Interfaces
{
    public interface IMicrophone
    {
        void PrepareRecording();
        void StartRecording();
        void StopRecording();
        void EndRecording();
        void PlayAudio();
        byte[] GetBytesArray();
    }

    public interface IAudioRecorder
    {
        bool IsRecording { get; set; }
        void StartRecording();
        void StopRecording();
    }
}
