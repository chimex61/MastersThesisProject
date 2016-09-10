using Android.Media;
using System.IO;
using System;
using System.Threading.Tasks;
using Android.OS;
using VoiceCommand.Droid;
using VoiceCommand.Interfaces;
using Stream = System.IO.Stream;

[assembly: Xamarin.Forms.Dependency(typeof(MicrophoneDroid))]

namespace VoiceCommand.Droid
{
    public class MicrophoneDroid : IMicrophone
    {
        private MediaRecorder m_oRecorder;
        private MediaPlayer m_oPlayer;
        private string m_oSaveRawPath = "/sdcard/record.raw";
        private string m_oSaveWavPath = "/sdcard/record.wav";

        public void PrepareRecording()
        {
            try
            {
                if (File.Exists(m_oSaveRawPath))
                {
                    File.Delete(m_oSaveRawPath);
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
                m_oRecorder.SetOutputFormat(OutputFormat.RawAmr);
                m_oRecorder.SetAudioEncoder(AudioEncoder.Default);
//                m_oRecorder.SetOutputFormat(OutputFormat.Mpeg4);
//                m_oRecorder.SetAudioEncoder(AudioEncoder.Default);
                m_oRecorder.SetOutputFile(m_oSaveRawPath);
                m_oRecorder.SetMaxDuration(10000); // 10sec
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
            m_oPlayer.SetDataSource(m_oSaveRawPath);
            m_oPlayer.Prepare();
            m_oPlayer.Start();
        }

        public void EndRecording()
        {
            m_oRecorder.Release();
        }

        public byte[] GetBytesArray()
        {
            FileStream oFileRawStream = new FileStream(m_oSaveRawPath, FileMode.Open, FileAccess.Read);
            FileStream oFileWavStream = new FileStream(m_oSaveWavPath, FileMode.Create);
            BinaryReader oFileReader = new BinaryReader(oFileRawStream);
            byte[] baAudioFile = oFileReader.ReadBytes((Int32) oFileRawStream.Length);

            oFileRawStream.Close();
            oFileReader.Close();

            byte[] header = WriteWaveFileHeader(baAudioFile.Length, baAudioFile.Length+44, 44100, 2, 176400);

            BinaryWriter oFileWriter = new BinaryWriter(oFileWavStream);
           
            oFileWriter.Write(baAudioFile);
            oFileWriter.Write(header, 0, 44);

            oFileWavStream.Close();
            oFileWriter.Close();

            oFileWavStream = new FileStream(m_oSaveRawPath, FileMode.Open, FileAccess.Read);
            oFileReader = new BinaryReader(oFileWavStream);

            byte[] baWavFile = oFileReader.ReadBytes((Int32) oFileWavStream.Length);

            oFileWavStream.Close();
            oFileReader.Close();

            return baWavFile;
        }

        // not sure if needed
        private byte[] WriteWaveFileHeader(
            long totalAudioLen,
            long totalDataLen, long longSampleRate, int channels,
            long byteRate)
        {

            byte[] header = new byte[44];

            header[0] = (byte) 'R'; // RIFF/WAVE header
            header[1] = (byte) 'I';
            header[2] = (byte) 'F';
            header[3] = (byte) 'F';
            header[4] = (byte) (totalDataLen & 0xff);
            header[5] = (byte) ((totalDataLen >> 8) & 0xff);
            header[6] = (byte) ((totalDataLen >> 16) & 0xff);
            header[7] = (byte) ((totalDataLen >> 24) & 0xff);
            header[8] = (byte) 'W';
            header[9] = (byte) 'A';
            header[10] = (byte) 'V';
            header[11] = (byte) 'E';
            header[12] = (byte) 'f'; // 'fmt ' chunk
            header[13] = (byte) 'm';
            header[14] = (byte) 't';
            header[15] = (byte) ' ';
            header[16] = 16; // 4 bytes: size of 'fmt ' chunk
            header[17] = 0;
            header[18] = 0;
            header[19] = 0;
            header[20] = 1; // format = 1
            header[21] = 0;
            header[22] = (byte) channels;
            header[23] = 0;
            header[24] = (byte) (longSampleRate & 0xff);
            header[25] = (byte) ((longSampleRate >> 8) & 0xff);
            header[26] = (byte) ((longSampleRate >> 16) & 0xff);
            header[27] = (byte) ((longSampleRate >> 24) & 0xff);
            header[28] = (byte) (byteRate & 0xff);
            header[29] = (byte) ((byteRate >> 8) & 0xff);
            header[30] = (byte) ((byteRate >> 16) & 0xff);
            header[31] = (byte) ((byteRate >> 24) & 0xff);
            header[32] = (byte) (2*16/8); // block align
            header[33] = 0;
            header[34] = 16; // bits per sample
            header[35] = 0;
            header[36] = (byte) 'd';
            header[37] = (byte) 'a';
            header[38] = (byte) 't';
            header[39] = (byte) 'a';
            header[40] = (byte) (totalAudioLen & 0xff);
            header[41] = (byte) ((totalAudioLen >> 8) & 0xff);
            header[42] = (byte) ((totalAudioLen >> 16) & 0xff);
            header[43] = (byte) ((totalAudioLen >> 24) & 0xff);

            return header;
        }
    }

    public class MicrophoneRecorderDroid : IAudioRecorder
    {

        public MicrophoneRecorderDroid()
        {
            IsRecording = false;
        }

        private AudioRecord audRecorder;
        private byte[] audioBuffer;
        private int audioData;
        public bool IsRecording { get; set; }
        private BinaryWriter bWriter;

        private string wavPath = "/sdcard/record.wav";

        public void StartRecording()
        {
            System.IO.Stream outputStream = System.IO.File.Open(wavPath, FileMode.Create);
            bWriter = new BinaryWriter(outputStream);
            audioBuffer = new byte[44100*5]; // 44100 sample rate * 10 sek (max time)

            audRecorder = new AudioRecord(AudioSource.Mic,
                44100,
                ChannelIn.Mono,
                Android.Media.Encoding.Pcm16bit,
                audioBuffer.Length);

//            long longSampleRate = 44100;
//            int channels = 2;
//            long byteRate = 16*longSampleRate*channels/8;
//
//            long totalAudioLen = audioBuffer.Length;
//            long totalDataLen = totalAudioLen + 36;
//
//            WriteWaveFileHeader(bWriter,
//                totalAudioLen,
//                totalDataLen,
//                longSampleRate,
//                channels,
//                byteRate);

            IsRecording = true;
            audRecorder.StartRecording();

            SaveBinaryAudio(outputStream);
        }

        private async Task SaveBinaryAudio(Stream outputStream)
        {
            while (IsRecording == true)
            {
                try
                {
                    audioData = audRecorder.Read(audioBuffer, 0, audioBuffer.Length);
                    bWriter.Write(audioBuffer);
                }
                catch (Exception ex)
                {
                    System.Console.Out.WriteLine(ex.Message);
                }
            }

            long longSampleRate = 44100;
            int channels = 2;
            long byteRate = 16 * longSampleRate * channels / 8;

            long totalAudioLen = audioBuffer.Length;
            long totalDataLen = totalAudioLen + 36;

            WriteWaveFileHeader(bWriter,
                totalAudioLen,
                totalDataLen,
                longSampleRate,
                channels,
                byteRate);

            outputStream.Close();
            bWriter.Close();
        }

        public void StopRecording()
        {
            IsRecording = false;
            audRecorder.Stop();
        }


        private void WriteWaveFileHeader(BinaryWriter bWriter,
                                         long totalAudioLen,
                                         long totalDataLen, 
                                         long longSampleRate,
                                         int channels,
                                         long byteRate)
        {
            byte[] header = new byte[44];

            header[0] = (byte)'R'; // RIFF/WAVE header
            header[1] = (byte)'I';
            header[2] = (byte)'F';
            header[3] = (byte)'F';
            header[4] = (byte)(totalDataLen & 0xff); // file size 4 - 7
            header[5] = (byte)((totalDataLen >> 8) & 0xff);
            header[6] = (byte)((totalDataLen >> 16) & 0xff);
            header[7] = (byte)((totalDataLen >> 24) & 0xff);
            header[8] = (byte)'W';
            header[9] = (byte)'A';
            header[10] = (byte)'V';
            header[11] = (byte)'E';
            header[12] = (byte)'f'; // 'fmt ' chunk
            header[13] = (byte)'m';
            header[14] = (byte)'t';
            header[15] = (byte)' ';
            header[16] = 16; // 4 bytes: size of 'fmt ' chunk
            header[17] = 0;
            header[18] = 0;
            header[19] = 0;
            header[20] = 1; // format (PCM) = 1
            header[21] = 0;
            header[22] = (byte)channels;
            header[23] = 0;
            header[24] = (byte)(longSampleRate & 0xff);
            header[25] = (byte)((longSampleRate >> 8) & 0xff);
            header[26] = (byte)((longSampleRate >> 16) & 0xff);
            header[27] = (byte)((longSampleRate >> 24) & 0xff);
            header[28] = (byte)(byteRate & 0xff);
            header[29] = (byte)((byteRate >> 8) & 0xff);
            header[30] = (byte)((byteRate >> 16) & 0xff);
            header[31] = (byte)((byteRate >> 24) & 0xff);
            header[32] = (byte)(2 * 16 / 8); // block align
            header[33] = 0;
            header[34] = 16; // bits per sample
            header[35] = 0;
            header[36] = (byte)'d';
            header[37] = (byte)'a';
            header[38] = (byte)'t';
            header[39] = (byte)'a';
            header[40] = (byte)(totalAudioLen & 0xff);
            header[41] = (byte)((totalAudioLen >> 8) & 0xff);
            header[42] = (byte)((totalAudioLen >> 16) & 0xff);
            header[43] = (byte)((totalAudioLen >> 24) & 0xff);

            bWriter.Write(header, 0, 44);
        }
    }
}