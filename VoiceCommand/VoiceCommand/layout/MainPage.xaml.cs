using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace VoiceCommand
{
    public partial class MainPage : ContentPage
    {
        WitAiServ m_oWit = new WitAiServ();
        OortServ m_oOort;
        IbmBlueMixServ m_oIbm = new IbmBlueMixServ();

        bool m_bRecording = false;

        public MainPage( OortServ oServ)
        {
            InitializeComponent();

            m_oOort = oServ;
        }

        private async void OnWitAiButtonClicked(object sender, EventArgs eventArgs)
        {
            if (!m_bRecording)
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
            WitAiButton.Text = "Stop";
//            m_oMic.ListDevices();
//            m_oMic.RecordAndSaveWin();
        }

        private async Task WitAiInactive()
        {
            DLButton.IsEnabled = true;
            WitAiButton.Text = "Wit.ai";
//            m_oMic.StopRecording();
//            byte[] baAudioFile = m_oMic.ProcessSpeech();
//            var oResponseStruct = /*await*/ m_oWit.ExecuteItem(baAudioFile);

            Task.WaitAll();
//            m_oOort.MakeAction(oResponseStruct);

        }

        private async void OnDLButtonClicked(object sender, EventArgs eventArgs)
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
            DLButton.Text = "Stop";
//            m_oMic.ListDevices();
//            m_oMic.RecordAndSaveWin();
        }

        private async Task IbmInactive()
        {
            WitAiButton.IsEnabled = true;
            DLButton.Text = "IBM Watson";
//            m_oMic.StopRecording();
//            byte[] baAudioFile = m_oMic.ProcessSpeech();
//            var ResponseStruct = m_oIbm.ExecuteItem(baAudioFile);
            //m_oIbm.ExecuteItem();

            Task.WaitAll();
            //m_oIbm.MakeAction( oResponseStruct );

        }
    }
}
