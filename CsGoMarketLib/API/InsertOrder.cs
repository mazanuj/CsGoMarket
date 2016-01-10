using System;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.DataTypes;
using CsGoMarketLib.Utilities;
using Newtonsoft.Json.Linq;

namespace CsGoMarketLib.API
{
    public static class InsertOrder
    {
        public static async Task<bool> Insert(string key, ItemStruct item)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.Uid))
                {
                    var unicodeString =
                        await
                            new WebClient().DownloadStringTaskAsync(
                                $"https://market.csgo.com/api/InsertOrder/{item.Classid}/{item.Instanceid}/{item.NormCost}/{item.Uid}/?key={key}");
                    JObject resp = await UnicodeConverter.Convert(unicodeString);

                    JToken token;
                    var success = resp.TryGetValue("success", out token);

                    if (success)
                        return true;

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