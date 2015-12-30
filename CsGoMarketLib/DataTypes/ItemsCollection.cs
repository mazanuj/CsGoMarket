using System.Collections.Generic;

namespace CsGoMarketLib.DataTypes
{
    public class ItemsCollection
    {
        public ItemsCollection()
        {
            ItemsList = new List<ItemStruct>();
        }

        public List<ItemStruct> ItemsList { get; set; }
    }
}