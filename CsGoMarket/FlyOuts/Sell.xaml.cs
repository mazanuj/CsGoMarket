using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
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
        public Sell()
        {
            var items = Utils.ItemsCollection.Where(x => x.SalesType == SalesEnum.Sell).ToList();
            Utils.SellCollection = new ObservableCollection<ItemStruct>(items);
            DataContext = this;
            InitializeComponent();
            DataGrid.IsSynchronizedWithCurrentItem = true;
            DataGrid.ItemsSource = Utils.SellCollection;
        }

        private void DataGrid_OnAddingNewItem(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    var newItem = e.Row.DataContext as ItemStruct;
                    newItem.SalesType = SalesEnum.Sell;
                }
            }
            catch (Exception)
            {
                Utils.SellCollection.AsParallel().ForAll(x => x.SalesType = SalesEnum.Sell);
            }
        }
    }
}