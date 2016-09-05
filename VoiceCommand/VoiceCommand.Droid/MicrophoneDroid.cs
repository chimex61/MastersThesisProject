using Android.Media;
using System.IO;
using System;
using VoiceCommand.Droid;
using VoiceCommand.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(MicrophoneDroid))]
namespace VoiceCommand.Droid
{
    public class MicrophoneDroid : IMicrophone
    {
        private MediaRecorder m_oRecorder;
        private MediaPlayer m_oPlayer;
        private string m_oSavePath = "/sdcard/record.3gp";

        public void PrepareRecording()
        {
            try
            {
                if (File.Exists(m_oSavePath))
                {
                    File.Delete(m_oSavePath);
                }


                if (m_oRecorder == null)
                {
                    m_oRecorder = new MediaRecorder();
                }
                else
                {
                    m_oRecorder.Reset();
                }
                m_oRecorder.SetAudioSource(AudioSource.Mic);
                m_oRecorder.SetOutputFormat(OutputFormat.ThreeGpp);
                m_oRecorder.SetAudioEncoder(AudioEncoder.AmrNb);
//                m_oRecorder.SetOutputFormat(OutputFormat.Mpeg4);
//                m_oRecorder.SetAudioEncoder(AudioEncoder.Default);
                m_oRecorder.SetOutputFile(m_oSavePath);
                m_oRecorder.Prepare();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Error: " + ex.Message);
            }
        }

        public void StartRecording()
        {
            m_oRecorder.Start();
        }

        public void StopRecording()
        {
            m_oRecorder.Stop();
            m_oRecorder.Reset();
        }

        public void PlayAudio()
        {
            if (m_oPlayer == null)
            {
                m_oPlayer = new MediaPlayer();
            }
            m_oPlayer.SetDataSource(m_oSavePath);
            m_oPlayer.Prepare();
            m_oPlayer.Start();
        }

        public void EndRecording()
        {
            m_oRecorder.Release();
        }
    }
}