using System;
using System.Collections.ObjectModel;
using System.Windows;
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
            DataContext = this;
            InitializeComponent();
            DataGrid.IsSynchronizedWithCurrentItem = true;
            DataGrid.ItemsSource = ObservableCollection;

            Informer.OnResultStr += result =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() => ObservableCollection.Insert(0, result)))
                    .Wait();
            };
        }
    }
}