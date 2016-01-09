using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using CsGoMarketLib.API;
using CsGoMarketLib.DataTypes;
using CsGoMarketLib.Sell;
using CsGoMarketLib.Utilities;

namespace CsGoMarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Timer UpdateTimer { get; }

        public MainWindow()
        {
            //Deserialize MenuCollection
            try
            {
                using (var fs = new FileStream("ItemsCollection.xml", FileMode.OpenOrCreate))
                    Utils.ItemsCollection =
                        ((ItemsCollection) new XmlSerializer(typeof (ItemsCollection)).Deserialize(fs)).ItemsList;
            }
            catch (Exception)
            {
            }

            UpdateTimer = new Timer
            {
                AutoReset = true,
                Interval = 5000
            };

            //UpdateTimer.Elapsed += CaptchaTimer_Elapsed;
            //UpdateTimer.Start();
            DataContext = this;
            InitializeComponent();

            ButtonStart.IsEnabled = true;
            ButtonStop.IsEnabled = false;
        }

        private void LaunchCsGoMarketOnGitHub(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/mazanuj/CsGoMarket");
        }

        private async void ButtonStart_OnClick(object sender, RoutedEventArgs e)
        {
            Utils.IsPermit = true;
            ButtonStart.IsEnabled = false;
            ButtonStop.IsEnabled = true;

            try
            {
                while (Utils.IsPermit)
                {
                    await SellCycle.ChangeSellItemPrice();
                    await SellerList.Get(Utils.SecretKey);
                }
            }
            catch (Exception ex)
            {
              Informer.RaiseOnResultReceived(ex);
            }
            ButtonStart.IsEnabled = true;
            ButtonStop.IsEnabled = false;
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null) Utils.SecretKey = textBox.Text;
        }

        private void ButtonStop_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonStop.IsEnabled = false;
            Utils.IsPermit = false;
        }

        private static async void CaptchaTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                await AutoUpdate.Update(Utils.SecretKey);
            }
            catch (Exception) { }
            await SellerList.Get(Utils.SecretKey);
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            UpdateTimer.Stop();
            UpdateTimer.Elapsed -= CaptchaTimer_Elapsed;
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            await SellerList.Get(Utils.SecretKey);
        }
    }
}