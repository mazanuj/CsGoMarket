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
        public readonly ObservableCollection<string> ObservableCollection = new ObservableCollection<string>();

        public Log()
        {
            DataContext = this;
            InitializeComponent();
            DataGrid.IsSynchronizedWithCurrentItem = true;
            DataGrid.ItemsSource = ObservableCollection;

            Informer.OnResultStr += async result =>
            {
                try
                {
                    await
                        Application.Current.Dispatcher.BeginInvoke(
                            new Action(() => ObservableCollection.Add(result)));
                }
                catch (Exception)
                {

                }
            };
        }
    }
}