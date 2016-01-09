using System.Collections.Generic;
using System.Collections.ObjectModel;
using CsGoMarketLib.DataTypes;

namespace CsGoMarketLib.Utilities
{
    public static class Utils
    {
        static Utils()
        {
            ItemsCollection = new List<ItemStruct>();
            SellCollection = new ObservableCollection<ItemStruct>();
            BuyCollection = new ObservableCollection<ItemStruct>();
        }

        public static bool IsPermit { get; set; }
        public static string SecretKey { get; set; }
        public static List<ItemStruct> ItemsCollection { get; set; }
        public static ObservableCollection<ItemStruct> SellCollection { get; set; }
        public static ObservableCollection<ItemStruct> BuyCollection { get; set; }
    }
}