using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PFD.UI.Authentication
{
    public sealed partial class Auth_Main : Page
    {
        private AuthenticationPage RootPage { get; } = AuthenticationPage.Current;

        public Auth_Main()
        {
            InitializeComponent();
            RootPage.UpdatePageTitle("Authentication");
            RootPage.UpdateSubpageTitle("Authentication");
        }

        private void BtnAuthByCard_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Auth_Card));

        private void BtnAuthByQr_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Auth_Qr));
    }
}
