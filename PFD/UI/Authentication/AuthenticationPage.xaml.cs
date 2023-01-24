using PFD.Component;
using System;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PFD.UI.Authentication
{
    internal sealed partial class AuthenticationPage : Page
    {
        private SpeechRecognizer SpeechRecognizer;
        private bool IsListening { get; set; } = false;
        private StringBuilder SpeechTextBuilder { get; } = new StringBuilder();

        public AuthenticationPage() => InitializeComponent();

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            bool hasPermission = await AudioCapturePermission.RequestMicrophonePermission();

            if (!hasPermission)
            {
                tbSpeech.Text = "Access to capture microphone resources was not given by the user";
                return;
            }

            await InitSpeechRecognizer();
            await StartRecognizing();

            fvContent.Navigate(typeof(Auth_Main));
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e) => await ReleaseSpeechRecognizerResource();

        private async Task ReleaseSpeechRecognizerResource()
        {
            if (null != SpeechRecognizer)
            {
                if (IsListening)
                {
                    await SpeechRecognizer.ContinuousRecognitionSession.CancelAsync();
                    IsListening = false;
                }

                SpeechRecognizer.StateChanged -= Recognizer_StateChanged;
                SpeechRecognizer.ContinuousRecognitionSession.Completed -= RecognizerSession_Completed;
                SpeechRecognizer.ContinuousRecognitionSession.ResultGenerated -= RecognizerSession_ResultGenerated;
                SpeechRecognizer.HypothesisGenerated -= Recognizer_HypothesisGenerated;

                SpeechRecognizer.Dispose();
                SpeechRecognizer = null;
            }
        }

        private async Task InitSpeechRecognizer()
        {
            await ReleaseSpeechRecognizerResource();
            SpeechRecognizer = new SpeechRecognizer(SpeechRecognizer.SystemSpeechLanguage);

            SpeechRecognitionTopicConstraint dictationConstraint = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "dictation");
            SpeechRecognizer.Constraints.Add(dictationConstraint);

            SpeechRecognitionCompilationResult result = await SpeechRecognizer.CompileConstraintsAsync();
            if (result.Status != SpeechRecognitionResultStatus.Success)
            {
                tbSpeech.Text = $"Grammar Compilation Failed: {result.Status}";
            }

            SpeechRecognizer.StateChanged += Recognizer_StateChanged;
            SpeechRecognizer.ContinuousRecognitionSession.Completed += RecognizerSession_Completed;
            SpeechRecognizer.ContinuousRecognitionSession.ResultGenerated += RecognizerSession_ResultGenerated;
            SpeechRecognizer.HypothesisGenerated += Recognizer_HypothesisGenerated;

            await StartRecognizing();
        }

        private async Task StartRecognizing()
        {
            if (!IsListening)
            {
                if (SpeechRecognizerState.Idle == SpeechRecognizer.State)
                {
                    IsListening = true;

                    try { await SpeechRecognizer.ContinuousRecognitionSession.StartAsync(); }
                    catch (Exception exception)
                    {
                        var messageDialog = new MessageDialog(exception.Message, "Exception");
                        await messageDialog.ShowAsync();

                        IsListening = false;
                    }
                }
            }
        }

        private async void Recognizer_StateChanged(SpeechRecognizer recognizer, SpeechRecognizerStateChangedEventArgs e) => await Dispatcher.RunAsync(CoreDispatcherPriority.Low, () => tbMicrophoneStatus.Text = $"Microphone: {e.State}");

        private async void Recognizer_HypothesisGenerated(SpeechRecognizer recognizer, SpeechRecognitionHypothesisGeneratedEventArgs e) => await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => tbSpeech.Text = $"{SpeechTextBuilder} {e.Hypothesis.Text} ...");

        private async void RecognizerSession_ResultGenerated(SpeechContinuousRecognitionSession session, SpeechContinuousRecognitionResultGeneratedEventArgs e)
        {
            SpeechRecognitionConfidence confidence = e.Result.Confidence;

            if (SpeechRecognitionConfidence.Medium == confidence || SpeechRecognitionConfidence.High == confidence)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    SpeechTextBuilder.Clear();
                    tbSpeech.Text = $"{e.Result.Text}";
                });
            }
        }

        private async void RecognizerSession_Completed(SpeechContinuousRecognitionSession session, SpeechContinuousRecognitionCompletedEventArgs e)
        {
            IsListening = false;
            await StartRecognizing();
        }
    }
}
