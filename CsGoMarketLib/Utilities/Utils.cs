using System.Collections.Generic;
using CsGoMarketLib.DataTypes;

namespace CsGoMarketLib.Utilities
{
    public static class Utils
    {
        static Utils()
        {
            ItemsCollection = new List<ItemStruct>();
        }

        public static List<ItemStruct> ItemsCollection { get; set; }
    }
}