using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CsGoMarketLib.DataTypes;
using CsGoMarketLib.Enums;
using CsGoMarketLib.Utilities;

namespace CsGoMarket.FlyOuts
{
    /// <summary>
    /// Interaction logic for Sell.xaml
    /// </summary>
    public partial class Sell
    {
        public ObservableCollection<ItemStruct> ObservableItems { get; set; }

        public Sell()
        {
            var items = Utils.ItemsCollection.Where(x => x.SalesType == SalesEnum.Sell).ToList();
            if (items.Any())
                items = new List<ItemStruct>();
            ObservableItems = new ObservableCollection<ItemStruct>(items);
            DataContext = this;
            InitializeComponent();
            DataGrid.IsSynchronizedWithCurrentItem = true;
            DataGrid.ItemsSource = ObservableItems;
        }
    }
}