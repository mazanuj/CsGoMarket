using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using CsGoMarketLib.API;
using CsGoMarketLib.Buy;
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

            UpdateTimer.Elapsed += UpdateTimer_Elapsed;
            UpdateTimer.Start();
            DataContext = this;
            InitializeComponent();

            ButtonStartSell.IsEnabled = true;
            ButtonStartBuy.IsEnabled = true;
            ButtonStop.IsEnabled = false;
        }

        private void LaunchCsGoMarketOnGitHub(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/mazanuj/CsGoMarket");
        }

        private async void ButtonStartSell_OnClick(object sender, RoutedEventArgs e)
        {
            Utils.IsPermit = true;
            ButtonStartSell.IsEnabled = false;
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

            ButtonStartSell.IsEnabled = true;
            ButtonStop.IsEnabled = false;
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null) Utils.SecretKey = textBox.Text;
        }

        private async void ButtonStop_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ButtonStop.IsEnabled = false;
                Utils.IsPermit = false;
                await RemoveAllOrders.Remove(Utils.SecretKey);
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
        }

        private async void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateTimer.Elapsed -= UpdateTimer_Elapsed;

            try
            {
                try
                {
                    await AutoUpdate.Update(Utils.SecretKey);
                }
                catch (Exception)
                {
                }

                await SellerList.Get(Utils.SecretKey);
                await BuyerList.Get(Utils.SecretKey);
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
            finally
            {
                UpdateTimer.Elapsed += UpdateTimer_Elapsed;
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            UpdateTimer.Stop();
            UpdateTimer.Elapsed -= UpdateTimer_Elapsed;
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            await SellerList.Get(Utils.SecretKey);
            await BuyerList.Get(Utils.SecretKey);
        }

        private async void ButtonStartBuy_OnClick(object sender, RoutedEventArgs e)
        {
            Utils.IsPermit = true;
            ButtonStartBuy.IsEnabled = false;
            ButtonStop.IsEnabled = true;

            try
            {
                foreach (var y in Utils.BuyCollection.Where(x => !x.Status))
                {
                    await InsertOrder.Insert(Utils.SecretKey, y);
                    y.Status = true;
                    y.IsAutoBuy = true;
                }

                while (Utils.IsPermit)
                {
                    await BuyCycle.ChangeBuyItemPrice();
                    await BuyerList.Get(Utils.SecretKey);
                }
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
            ButtonStartBuy.IsEnabled = true;
            ButtonStop.IsEnabled = false;
        }
    }
}