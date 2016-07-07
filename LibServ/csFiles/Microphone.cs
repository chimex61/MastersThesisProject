using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibServ
{
    public class Microphone
    {
        NAudio.Wave.WaveIn m_oSourceStream = null;
        NAudio.Wave.DirectSoundOut m_oWaveOut = null;
        NAudio.Wave.WaveFileWriter m_oWaveWriter = null;
        int m_iDeviceNumber = 0;
        string m_sFileName = "..//..//..//..//wav//record.wav";

        public void ListDevices()
        {
            List<NAudio.Wave.WaveInCapabilities> oSources = new List<NAudio.Wave.WaveInCapabilities>();

            for ( int iIterator = 0; iIterator < NAudio.Wave.WaveIn.DeviceCount; iIterator++ )
            {
                oSources.Add( NAudio.Wave.WaveIn.GetCapabilities( iIterator ));
            }
        }

        public void RecordAndPlay()
        {
            m_oSourceStream = new NAudio.Wave.WaveIn();
            m_oSourceStream.DeviceNumber = m_iDeviceNumber;
            m_oSourceStream.WaveFormat = new NAudio.Wave.WaveFormat( 44100, NAudio.Wave.WaveIn.GetCapabilities( m_iDeviceNumber ).Channels );

            NAudio.Wave.WaveInProvider oWaveIn = new NAudio.Wave.WaveInProvider( m_oSourceStream );

            m_oWaveOut = new NAudio.Wave.DirectSoundOut();
            m_oWaveOut.Init( oWaveIn );

            m_oSourceStream.StartRecording();
            m_oWaveOut.Play();
        }

        public void StopRecording()
        {
            if ( m_oWaveOut != null )
            {
                m_oWaveOut.Stop();
                m_oWaveOut.Dispose();
                m_oWaveOut = null;
            }
            if ( m_oSourceStream != null )
            {
                m_oSourceStream.StopRecording();
                m_oSourceStream.Dispose();
                m_oSourceStream = null;
            }
            if ( m_oWaveWriter != null )
            {
                m_oWaveWriter.Dispose();
                m_oWaveWriter = null;
            }
        }

        public void RecordAndSaveWin()
        {
            m_oSourceStream = new NAudio.Wave.WaveIn();
            m_oSourceStream.DeviceNumber = m_iDeviceNumber;
            m_oSourceStream.WaveFormat = new NAudio.Wave.WaveFormat( 44100, NAudio.Wave.WaveIn.GetCapabilities( m_iDeviceNumber ).Channels );

            m_oSourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>( SourceStreamDataAvailableEvent );
            m_oWaveWriter = new NAudio.Wave.WaveFileWriter( m_sFileName, m_oSourceStream.WaveFormat );

            m_oSourceStream.StartRecording();
        }

        private void SourceStreamDataAvailableEvent( object sender, NAudio.Wave.WaveInEventArgs e )
        {
            if ( m_oWaveWriter == null ) return;

            m_oWaveWriter.Write( e.Buffer, 0, e.BytesRecorded );
            m_oWaveWriter.Flush();
        }

        public byte[] ProcessSpeech()
        {
            FileStream oFileStream = new FileStream( m_sFileName, FileMode.Open, FileAccess.Read );
            BinaryReader oFileReader = new BinaryReader( oFileStream );
            byte[] baAudioFile = oFileReader.ReadBytes(( Int32 )oFileStream.Length );
            oFileStream.Close();
            oFileReader.Close();

            return baAudioFile;
        }
    }
}
