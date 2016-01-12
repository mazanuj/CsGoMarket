using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using CsGoMarketLib.API;
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
                if (e.EditAction != DataGridEditAction.Commit) return;

                var newItem = e.Row.DataContext as ItemStruct;
                if (newItem != null) newItem.SalesType = SalesEnum.Buy;
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
                Utils.BuyCollection.AsParallel().ForAll(x => x.SalesType = SalesEnum.Buy);
            }
        }

        private async void Rectangle_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var rect = (Rectangle) sender;
                if (rect == null) return;

                for (var i = 0; i < DataGrid.Items.Count; i++)
                {
                    var row = (DataGridRow) DataGrid.ItemContainerGenerator.ContainerFromIndex(i);

                    ItemStruct item;
                    var rectangle = Utils.FindChild<Rectangle>(row, "rect", out item);

                    if (string.IsNullOrEmpty(item.Uid))
                        return;

                    if (rectangle == null || !Equals(rectangle, rect)) continue;

                    var newResult = !item.Status;
                    item.IsAutoBuy = newResult;

                    var result = newResult
                        ? await InsertOrder.Insert(Utils.SecretKey, item)
                        : await UpdateOrder.Update(Utils.SecretKey, item, 0);

                    if (result)
                        item.Status = newResult;
                    break;
                }
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
        }
    }
}