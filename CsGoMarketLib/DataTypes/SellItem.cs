namespace CsGoMarketLib.DataTypes
{
    public class SellItem
    {
        public string I_classid { get; set; }
        public string I_instanceid { get; set; }
        public string Ui_id { get; set; }
        public int Min_price { get; set; }
        public int Position { get; set; }
        public int MinCost { get; set; }
        public int CurrCost { get; set; }
    }
}