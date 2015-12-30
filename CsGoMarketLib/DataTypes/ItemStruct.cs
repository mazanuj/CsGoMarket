using System.ComponentModel;
using System.Runtime.CompilerServices;
using CsGoMarket.Annotations;
using CsGoMarketLib.Enums;

namespace CsGoMarketLib.DataTypes
{
    public class ItemStruct : INotifyPropertyChanged
    {
        private int currCost;
        private SalesEnum salesType;

        public string Name { get; set; }
        public string Classid_instanceid { get; set; }
        public int MinCost { get; set; }
        public int NormCost { get; set; }

        public int CurrCost
        {
            get { return currCost; }
            set
            {
                currCost = value;
                OnPropertyChanged();
            }
        }

        public SalesEnum SalesType
        {
            get { return salesType; }
            set
            {
                salesType = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}