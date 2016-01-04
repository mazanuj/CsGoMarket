using System.Collections.ObjectModel;
using CsGoMarketLib.Utilities;

namespace CsGoMarket.FlyOuts
{
    /// <summary>
    /// Interaction logic for Log.xaml
    /// </summary>
    public partial class Log
    {
        private readonly ObservableCollection<string> ObservableCollection = new ObservableCollection<string>();

        public Log()
        {
            Informer.OnResultStr += result => { ObservableCollection.Add(result); };

            DataContext = this;
            InitializeComponent();
            DataGrid.IsSynchronizedWithCurrentItem = true;
            DataGrid.ItemsSource = ObservableCollection;
        }
    }
}