using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LibServ;

namespace VoiceCommand.Win
{
    public partial class MainPage : Page
    {
        Microphone m_oMic = new Microphone();
        WitAiServ m_oWit = new WitAiServ();
        OortServ m_oOort;
        IbmBlueMixServ m_oIbm = new IbmBlueMixServ();

        bool m_bRecording = false;

        public MainPage( OortServ oOort )
        {
            InitializeComponent();
            m_oOort = oOort;
        }

        private async void OnWitAiButtonClicked( object sender, RoutedEventArgs e )
        {
            if ( !m_bRecording )
            {
                m_bRecording = true;
                WitAiActive();
            }
            else
            {
                m_bRecording = false;
                await WitAiInactive();
            }
        }

        private void WitAiActive()
        {
            DLButton.IsEnabled = false;
            WitAiButton.Content = "Stop";
            m_oMic.ListDevices();
            m_oMic.RecordAndSaveWin();
        }

        private async Task WitAiInactive()
        {
            DLButton.IsEnabled = true;
            WitAiButton.Content = "Wit.ai";
            m_oMic.StopRecording();
            byte[] baAudioFile = m_oMic.ProcessSpeech();
            var oResponseStruct = /*await*/ m_oWit.ExecuteItem(baAudioFile);

            Task.WaitAll();
            m_oOort.MakeAction( oResponseStruct );

        }

        private async void OnDLButtonClicked( object sender, RoutedEventArgs e )
        {
            if (!m_bRecording)
            {
                m_bRecording = true;
                IbmActive();
            }
            else
            {
                m_bRecording = false;
                await IbmInactive();
            }
        }

        private void IbmActive()
        {
            WitAiButton.IsEnabled = false;
            DLButton.Content = "Stop";
            m_oMic.ListDevices();
            m_oMic.RecordAndSaveWin();
        }

        private async Task IbmInactive()
        {
            WitAiButton.IsEnabled = true;
            DLButton.Content = "IBM Watson";
            m_oMic.StopRecording();
            byte[] baAudioFile = m_oMic.ProcessSpeech();
            var oResponseStruct = m_oIbm.ExecuteItem( baAudioFile );

            Task.WaitAll();
            m_oOort.MakeAction( oResponseStruct );
        }
    }
}
