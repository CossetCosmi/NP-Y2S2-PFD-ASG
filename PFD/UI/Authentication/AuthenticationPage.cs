using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PFD.UI.Authentication
{
    internal sealed partial class AuthenticationPage : Page
    {
        private bool IsSpeechVisible { get; set; } = false;

        #region OnClick Handler
        private void BtnMenu_Click(object sender, RoutedEventArgs e) => svAuthentication.IsPaneOpen = !svAuthentication.IsPaneOpen;

        private void CbSpeechText_Click(object sender, RoutedEventArgs e)
        {
            IsSpeechVisible = !IsSpeechVisible;

            if (IsSpeechVisible)
                Show_TbSpeech();
            else Hide_TbSpeech();
        }
        #endregion OnClick Handler

        #region Show/Hide Control
        private void Hide_TbSpeech() => tbSpeech.Visibility = Visibility.Collapsed;
        private void Show_TbSpeech() => tbSpeech.Visibility = Visibility.Visible;
        #endregion Show/Hide Control

        public void UpdatePageTitle(string title) => tbPageTitle.Text = title;
        public void UpdateSubpageTitle(string title) => tbSubpageTitle.Text = title;

        #region QR Page
        private void BtnQr_Click(object sender, RoutedEventArgs e) => btnQr.Visibility = Visibility.Collapsed;
        public void Show_QrPage_Popup() => btnQr.Visibility = Visibility.Visible;
        #endregion
    }
}
