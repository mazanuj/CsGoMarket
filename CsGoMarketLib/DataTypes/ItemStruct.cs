using System.ComponentModel;
using System.Runtime.CompilerServices;
using CsGoMarket.Annotations;
using CsGoMarketLib.Enums;

namespace CsGoMarketLib.DataTypes
{
    public class ItemStruct : INotifyPropertyChanged
    {

        private SalesEnum salesType;
        private bool status;
        private int normCost;
        private int limitCost;
        private int currCost;
        private string name;
        private string instanceid;
        private string classid;
        private string uid;
        private int min_price;
        private int position;

        public string Uid
        {
            get { return uid; }
            set
            {
                uid = value;
                OnPropertyChanged();
            }
        }

        public int Min_price
        {
            get { return min_price; }
            set
            {
                min_price = value;
                OnPropertyChanged();
            }
        }

        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        public int LimitCost
        {
            get { return limitCost; }
            set
            {
                limitCost = value;
                OnPropertyChanged();
            }
        }

        public string Instanceid
        {
            get { return instanceid; }
            set
            {
                instanceid = value;
                OnPropertyChanged();
            }
        }

        public string Classid
        {
            get { return classid; }
            set
            {
                classid = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public int NormCost
        {
            get { return normCost; }
            set
            {
                normCost = value;
                OnPropertyChanged();
            }
        }

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

        public bool Status
        {
            get { return status; }
            set
            {
                status = value;
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