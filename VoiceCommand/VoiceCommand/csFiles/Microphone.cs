namespace VoiceCommand.Interfaces
{
    public interface IMicrophone
    {
        void PrepareRecording();
        void StartRecording();
        void StopRecording();
        void EndRecording();
        void PlayAudio();
    }
}
