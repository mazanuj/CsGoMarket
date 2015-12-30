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
    /// Interaction logic for Buy.xaml
    /// </summary>
    public partial class Buy
    {
        public Buy()
        {
            var items = Utils.ItemsCollection.Where(x => x.SalesType == SalesEnum.Buy).ToList();
            Utils.BuyCollection = new ObservableCollection<ItemStruct>(items);
            DataContext = this;
            InitializeComponent();
            DataGrid.IsSynchronizedWithCurrentItem = true;
            DataGrid.ItemsSource = Utils.BuyCollection;
        }

        private void DataGrid_OnAddingNewItem(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    var newItem = e.Row.DataContext as ItemStruct;
                    newItem.SalesType = SalesEnum.Buy;
                }
            }
            catch (Exception)
            {
                Utils.BuyCollection.AsParallel().ForAll(x => x.SalesType = SalesEnum.Buy);
            }
        }
    }
}