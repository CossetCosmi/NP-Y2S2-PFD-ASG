using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.UI.Popups;


namespace PFD.Component
{
    internal class AudioCapturePermission
    {
        private static int NoCaptureDevicesHResult { get; } = -1072845856;

        public async static Task<bool> RequestMicrophonePermission()
        {
            bool hasPermission = false;

            try
            {
                await new MediaCapture().InitializeAsync(new MediaCaptureInitializationSettings
                {
                    StreamingCaptureMode = StreamingCaptureMode.Audio,
                    MediaCategory = MediaCategory.Speech
                });

                hasPermission = true;
            }
            catch (TypeLoadException)
            {
                await new MessageDialog("Media player components are unavailable.").ShowAsync();
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception exception)
            {
                if (exception.HResult != NoCaptureDevicesHResult)
                    throw;
                else await new MessageDialog("No Audio Capture devices are present on this system.").ShowAsync();
            }

            return hasPermission;
        }
    }
}
