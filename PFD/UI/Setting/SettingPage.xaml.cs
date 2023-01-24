using PFD.Model;
using PFD.Repository;
using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PFD.UI.Setting
{
    public sealed partial class SettingPage : Page
    {
        #region Repository
        private AccountRepository AccountRepository { get; } = AccountRepository.Instance;
        private CardRepository CardRepository { get; } = CardRepository.Instance;
        #endregion Repository

        #region Properties
        private List<Account> AccountList { get; set; }
        private List<Card> CardList { get; set; }
        #endregion Properties

        #region Static Properties
        public static SettingPage Current;
        #endregion Static Properties

        #region Speech Text State Listener
        public List<Action<bool>> SpeechTextStateListenerList { get; } = new List<Action<bool>> { };
        #endregion Speech Text State Listener

        public SettingPage()
        {
            Current = this;

            InitializeComponent();
            InitializeAccountList();
        }

        private void InitializeAccountList() => AccountList = AccountRepository.FindAll();

        private void cbSpeech_Click(object sender, RoutedEventArgs e)
        {
            foreach (Action<bool> execute in SpeechTextStateListenerList)
            {
                execute((bool)(sender as CheckBox).IsChecked);
            }
        }

        private async void cbAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbAccount = sender as ComboBox;
            Account account = cbAccount.SelectedItem as Account;

            CardList = new List<Card>();

            foreach (Card card in CardRepository.FindAll())
                if (card.AccountId == account.Id)
                    CardList.Add(card);

            cbCard.IsEnabled = true;

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Bindings.Update());
        }

        private void cbCard_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}
