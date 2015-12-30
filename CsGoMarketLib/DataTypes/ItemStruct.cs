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
        private int minCost;
        private int currCost;
        private string classid_instanceid = string.Empty;
        private string name = string.Empty;

        public int MinCost
        {
            get { return minCost; }
            set
            {
                minCost = value;
                OnPropertyChanged();
            }
        }

        public string Classid_instanceid
        {
            get { return classid_instanceid; }
            set
            {
                classid_instanceid = value;
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