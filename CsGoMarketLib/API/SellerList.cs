using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.DataTypes;
using CsGoMarketLib.Utilities;
using Newtonsoft.Json.Linq;

namespace CsGoMarketLib.API
{
    public static class SellerList
    {
        public static async Task<List<SellItem>> Get(string key)
        {
            var list = new List<SellItem>();
            try
            {
                var unicodeString =
                    await new WebClient().DownloadStringTaskAsync($"https://market.csgo.com/api/Trades/?key={key}");
                JArray resp = await UnicodeConverter.Convert(unicodeString);

                list.AddRange(resp.Select(x => new SellItem
                {
                    I_classid = x["i_classid"].ToString(),
                    I_instanceid = x["i_instanceid"].ToString(),
                    Ui_id = x["ui_id"].ToString(),
                    Min_price = int.Parse(x["min_price"].ToString().Replace(".", string.Empty)),
                    Position = int.Parse(x["position"].ToString())
                }));
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
            return list;
        }
    }
}