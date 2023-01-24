using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.UI.Popups;


namespace PFD.Component
{
    internal static class AudioCapturePermission
    {
        private static int NoCaptureDevicesHResult { get; } = -1072845856;

        public async static Task<bool> RequestMicrophonePermission()
        {
            bool hasPermission = false;

            try
            {
                await
                    new MediaCapture().InitializeAsync(
                        new MediaCaptureInitializationSettings
                        {
                            StreamingCaptureMode = StreamingCaptureMode.Audio,
                            MediaCategory = MediaCategory.Speech
                        });

                hasPermission = true;
            }
            catch (TypeLoadException)
            {
                MessageDialog messageDialog = new MessageDialog("Media player components are unavailable.");
                await messageDialog.ShowAsync();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception exception)
            {
                if (exception.HResult != NoCaptureDevicesHResult)
                    throw;

                MessageDialog messageDialog = new MessageDialog("No Audio Capture devices are present on this system.");
                await messageDialog.ShowAsync();
            }

            return hasPermission;
        }
    }
}
