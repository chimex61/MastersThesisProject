using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibServ
{
    public class Microphone
    {
        NAudio.Wave.WaveIn sourceStream = null;
        NAudio.Wave.DirectSoundOut waveOut = null;
        NAudio.Wave.WaveFileWriter waveWriter = null;
        int deviceNumber = 0;
        string fileName = "..//..//..//..//wav//record.wav";

        public void ListDevices()
        {
            List<NAudio.Wave.WaveInCapabilities> sources = new List<NAudio.Wave.WaveInCapabilities>();

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }
        }

        public void RecordAndPlay()
        {
            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = deviceNumber;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

            NAudio.Wave.WaveInProvider waveIn = new NAudio.Wave.WaveInProvider(sourceStream);

            waveOut = new NAudio.Wave.DirectSoundOut();
            waveOut.Init(waveIn);

            sourceStream.StartRecording();
            waveOut.Play();
        }

        public void StopRecording()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }
            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }
        }

        public void RecordAndSaveWin()
        {
            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = deviceNumber;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(8800, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

            sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
            waveWriter = new NAudio.Wave.WaveFileWriter(fileName, sourceStream.WaveFormat);

            sourceStream.StartRecording();
        }

        private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }
    }
}
