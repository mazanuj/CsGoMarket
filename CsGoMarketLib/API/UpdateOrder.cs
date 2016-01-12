using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.DataTypes;
using CsGoMarketLib.Utilities;
using Newtonsoft.Json.Linq;

namespace CsGoMarketLib.API
{
    public static class UpdateOrder
    {
        public static async Task<bool> Update(string key, ItemStruct item, int cost)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.Uid))
                {
                    var unicodeString =
                        await
                            new WebClient().DownloadStringTaskAsync(
                                $"https://market.csgo.com/api/UpdateOrder/{item.Classid}/{item.Instanceid}/{cost}/?key={key}");
                    JObject resp = await UnicodeConverter.Convert(unicodeString);

                    JToken token;
                    var success = resp.TryGetValue("success", out token);

                    if (success)
                    {
                        //Informer.RaiseOnResultReceived($"Successfully update order {(double) cost/100}₽ for {item.Name}");
                        return true;
                    }

                    Informer.RaiseOnResultReceived(resp["error"].ToString());
                }
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
            return false;
        }
    }
}