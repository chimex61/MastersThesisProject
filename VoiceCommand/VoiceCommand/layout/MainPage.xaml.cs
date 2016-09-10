using System;
using System.Threading.Tasks;
using VoiceCommand.Interfaces;
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

        // http://stackoverflow.com/questions/27755271/how-to-invoke-a-method-located-inside-the-android-project-from-the-portable-clas
        private void WitAiActive()
        {
            DLButton.IsEnabled = false;
            WitAiButton.Text = "Stop";
            DependencyService.Get<IMicrophone>().PrepareRecording();
            DependencyService.Get<IMicrophone>().StartRecording();
            //DependencyService.Get<IAudioRecorder>().StartRecording();
        }

        private async Task WitAiInactive()
        {
            DLButton.IsEnabled = true;
            WitAiButton.Text = "Wit.ai";

            DependencyService.Get<IMicrophone>().StopRecording();
            byte [] baAudioFile = DependencyService.Get<IMicrophone>().GetBytesArray();


            var oResponceStruct = await m_oWit.ExecuteItem(baAudioFile);
            //m_oOort.MakeAction(oResponceStruct);
            //DependencyService.Get<IMicrophone>().PlayAudio();
            //DependencyService.Get<IMicrophone>().EndRecording();

            //DependencyService.Get<IAudioRecorder>().StopRecording();
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
