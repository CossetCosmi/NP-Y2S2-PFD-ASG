using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PFD.UI.Authentication
{
    public sealed partial class Auth_Qr : Page
    {
        private AuthenticationPage RootPage { get; } = AuthenticationPage.Current;

        public Auth_Qr() => InitializeComponent();

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.GoBack();

        private void BtnGenerateQR_Click(object sender, RoutedEventArgs e) => RootPage.Show_QrPage_Popup();

    }
}
