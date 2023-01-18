using System;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Globalization;
using Windows.Media.SpeechRecognition;
using PFD.Component;


namespace PFD.UI
{
    public sealed partial class WelcomePage : Page
    {
        private bool IsListening { get; set; }
        private SpeechRecognizer SpeechRecognizer { get; set; }
        private StringBuilder DictatedTextBuilder { get; }

        public WelcomePage()
        {
            InitializeComponent();
            IsListening = false;
            DictatedTextBuilder = new StringBuilder();
        }

        /// <summary>
        ///     Upon entering this ui page, ensure that we have premission to use the Microphone
        /// </summary>
        /// <param name="eventArgs">Ignored</param>
        protected override async void OnNavigatedTo(NavigationEventArgs eventArgs)
        {
            bool hasPermission = await AudioCapturePermission.RequestMicrophonePermission();

            if (hasPermission)
            {
                await InitializeSpeechRecognizer(SpeechRecognizer.SystemSpeechLanguage);
                await SpeechRecognizer.ContinuousRecognitionSession.StartAsync();
                IsListening = true;
            }
            else TbSpeech.Text = "Permission to access capture resources was not given by the user";
        }

        /// <summary>
        ///     Upon learning this ui page, clean up the speech recognizer.
        /// </summary>
        /// <param name="e">Ignored</param>
        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (null != SpeechRecognizer)
            {
                if (IsListening)
                {
                    await SpeechRecognizer.ContinuousRecognitionSession.CancelAsync();
                    IsListening = false;
                }

                TbSpeech.Text = "";
                SpeechRecognizer.ContinuousRecognitionSession.Completed -= SR_CRS_Completed;
                SpeechRecognizer.ContinuousRecognitionSession.ResultGenerated -= SR_CRS_ResultGenerated;
                SpeechRecognizer.HypothesisGenerated -= SR_HypothesisGenerated;

                SpeechRecognizer.Dispose();
                SpeechRecognizer = null;
            }
        }

        /// <summary>
        ///     Initialize Speech Recognizer
        /// </summary>
        /// <param name="language">Language to use for the speech recognizer</param>
        /// <returns>Awaitable task</returns>
        private async Task InitializeSpeechRecognizer(Language language)
        {
            if (null != SpeechRecognizer)
            {
                SpeechRecognizer.ContinuousRecognitionSession.Completed -= SR_CRS_Completed;
                SpeechRecognizer.ContinuousRecognitionSession.ResultGenerated -= SR_CRS_ResultGenerated;
                SpeechRecognizer.HypothesisGenerated -= SR_HypothesisGenerated;

                SpeechRecognizer.Dispose();
                SpeechRecognizer = null;
            }

            SpeechRecognizer = new SpeechRecognizer(language);
            SpeechRecognizer.ContinuousRecognitionSession.Completed += SR_CRS_Completed;
            SpeechRecognizer.ContinuousRecognitionSession.ResultGenerated += SR_CRS_ResultGenerated;
            SpeechRecognizer.HypothesisGenerated += SR_HypothesisGenerated;

            var constraint = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "dictation");
            SpeechRecognizer.Constraints.Add(constraint);
            SpeechRecognitionCompilationResult result = await SpeechRecognizer.CompileConstraintsAsync();

            if (SpeechRecognitionResultStatus.Success != result.Status)
            {
                TbSpeech.Text = "Grammar Compilation Failed: " + result.Status.ToString();
            }
        }

        /// <summary>
        /// Handle events fired when error conditions occur
        /// </summary>
        /// <param name="sender">The continuous recognition session</param>
        /// <param name="args">The state of the recognizer</param>
        private async void SR_CRS_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
        {
            if (SpeechRecognitionResultStatus.Success != args.Status)
            {
                await SpeechRecognizer.ContinuousRecognitionSession.StartAsync();
                IsListening = true;

                if (SpeechRecognitionResultStatus.TimeoutExceeded == args.Status)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TbSpeech.Text = DictatedTextBuilder.ToString());
                }
            }
        }

        /// <summary>
        /// While the user is speaking, update the textbox with the partial sentence of what's being said for user feedback.
        /// </summary>
        /// <param name="sender">The recognizer that has generated the hypothesis</param>
        /// <param name="args">The hypothesis formed</param>
        private async void SR_HypothesisGenerated(SpeechRecognizer sender, SpeechRecognitionHypothesisGeneratedEventArgs args)
        {
            string hypothesis = args.Hypothesis.Text;
            string textboxContent = $"{DictatedTextBuilder} {hypothesis} ...";

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TbSpeech.Text = textboxContent);
        }

        /// <summary>
        /// Handle events fired when a result is generated. Check for high to medium confidence, and then append the
        /// string to the end of the stringbuffer, and replace the content of the textbox with the string buffer, to
        /// remove any hypothesis text that may be present.
        /// </summary>
        /// <param name="sender">The Recognition session that generated this result</param>
        /// <param name="args">Details about the recognized speech</param>
        private async void SR_CRS_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            if (SpeechRecognitionConfidence.Medium == args.Result.Confidence ||
                SpeechRecognitionConfidence.High == args.Result.Confidence)
            {
                DictatedTextBuilder.Clear().Append($"{args.Result.Text} ");

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => TbSpeech.Text = $"{DictatedTextBuilder}");
            }
        }
    }
}
