using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.Utilities;
using Newtonsoft.Json.Linq;

namespace CsGoMarketLib.API
{
    public static class SellerList
    {
        public static async Task Get(string key)
        {
            try
            {
                var unicodeString =
                    await new WebClient().DownloadStringTaskAsync($"https://market.csgo.com/api/Trades/?key={key}");
                JArray resp = await UnicodeConverter.Convert(unicodeString);

                Utils.SellCollection.AsParallel().ForAll(x =>
                {
                    if (resp.Any(y => y["i_classid"].ToString() == x.Classid)) return;
                    x.Status = false;
                    x.CurrCost = 0;
                    x.Position = 0;
                    x.Min_price = 0;
                    x.Uid = "0";
                });

                foreach (var x in resp)
                {
                    try
                    {
                        var first = Utils.SellCollection.First(y => y.Classid == x["i_classid"].ToString() && y.Instanceid == x["i_instanceid"].ToString());
                        first.Uid = x["ui_id"].ToString();
                        first.Name = x["i_name"].ToString();
                        first.Min_price = (int) Math.Round(100*double.Parse(x["min_price"].ToString()), 2);
                        first.Position = int.Parse(x["position"].ToString());
                        first.Status = x["ui_status"].ToString() == "1";
                        first.CurrCost = (int)Math.Round(100 * double.Parse(x["ui_price"].ToString()), 2);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
        }
    }
}