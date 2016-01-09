using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.Utilities;
using Newtonsoft.Json.Linq;

namespace CsGoMarketLib.API
{
    public static class GetItemPrice
    {
        public static async Task<List<int>> Get(string classId, string instanceId, string key)
        {
            var list = new List<int>();
            try
            {
                var unicodeString =
                    await
                        new WebClient().DownloadStringTaskAsync(
                            $"https://market.csgo.com/api/ItemInfo/{classId}_{instanceId}/ru/?key={key}");
                var resp = await UnicodeConverter.Convert(unicodeString);

                JArray offers = resp["offers"];

                list.AddRange(offers.Select(x => int.Parse(x["price"].ToString())));
                list.Sort();
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
            return list;
        }
    }
}