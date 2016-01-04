using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using CsGoMarketLib.DataTypes;
using CsGoMarketLib.Utilities;

namespace CsGoMarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            //Deserialize MenuCollection
            try
            {
                using (var fs = new FileStream("ItemsCollection.xml", FileMode.OpenOrCreate))
                    Utils.ItemsCollection =
                        ((ItemsCollection)new XmlSerializer(typeof(ItemsCollection)).Deserialize(fs)).ItemsList;
            }
            catch (Exception)
            {
            }

            DataContext = this;
            InitializeComponent();            
        }

        private void LaunchCsGoMarketOnGitHub(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/mazanuj/CsGoMarket");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}