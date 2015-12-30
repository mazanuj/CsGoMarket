using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using CsGoMarket.Properties;
using CsGoMarketLib.DataTypes;
using CsGoMarketLib.Utilities;

namespace CsGoMarket
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnExit(ExitEventArgs e)
        {
            //Serialized ItemsCollection
            if (File.Exists("ItemsCollection.xml"))
                File.Delete("ItemsCollection.xml");

            Utils.ItemsCollection = new List<ItemStruct>();
            Utils.ItemsCollection.AddRange(Utils.SellCollection);
            Utils.ItemsCollection.AddRange(Utils.BuyCollection);

            var mi = new ItemsCollection {ItemsList = Utils.ItemsCollection};
            using (var fs = new FileStream("ItemsCollection.xml", FileMode.OpenOrCreate))
                new XmlSerializer(typeof (ItemsCollection)).Serialize(fs, mi);

            Settings.Default.Save();
            base.OnExit(e);
        }
    }
}