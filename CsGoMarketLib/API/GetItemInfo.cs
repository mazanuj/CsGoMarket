using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.Utilities;
using Newtonsoft.Json.Linq;

namespace CsGoMarketLib.API
{
    public static class GetItemInfo
    {
        public static async Task Get(string key)
        {
            try
            {
                foreach (
                    var y in
                        Utils.BuyCollection.Where(
                            x => !string.IsNullOrEmpty(x.Classid) && !string.IsNullOrEmpty(x.Instanceid)))
                {
                    try
                    {
                        var unicodeString =
                            await
                                new WebClient().DownloadStringTaskAsync(
                                    $"https://market.csgo.com/api/ItemInfo/{y.Classid}_{y.Instanceid}/ru/?key={key}");
                        var resp = await UnicodeConverter.Convert(unicodeString);

                        JArray offers = resp["buy_offers"];

                        y.Name = resp["name"].ToString();
                        y.Min_price = int.Parse(offers?.First()["o_price"].ToString() ?? "0");
                        y.Uid = resp["hash"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Informer.RaiseOnResultReceived(ex);
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