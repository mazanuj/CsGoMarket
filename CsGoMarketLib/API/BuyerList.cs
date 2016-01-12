using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.Utilities;
using Newtonsoft.Json.Linq;

namespace CsGoMarketLib.API
{
    public static class BuyerList
    {
        public static async Task Get(string key)
        {
            try
            {
                await GetItemInfo.Get(Utils.SecretKey);

                var unicodeString =
                    await new WebClient().DownloadStringTaskAsync($"https://market.csgo.com/api/GetOrders/?key={key}");
                var resp = await UnicodeConverter.Convert(unicodeString);                

                JArray arr;
                try
                {
                    arr = resp["Orders"];

                    if (arr == null)
                        throw new Exception();
                }
                catch (Exception)
                {
                    Utils.BuyCollection.AsParallel().ForAll(x =>
                    {
                        if (x.IsAutoBuy)
                        {
                            x.Status = true;
                            Task.Run(async () => { await InsertOrder.Insert(Utils.SecretKey, x); }).Wait();
                        }
                        else
                        {
                            x.Status = false;
                            x.CurrCost = 0;
                            x.Position = 0;
                        }
                    });

                    JToken token;
                    var jObject = resp as JObject;
                    if (jObject != null && jObject.TryGetValue("error", out token))
                        Informer.RaiseOnResultReceived($"{resp["error"].ToString()} in BuyerList");

                    return;
                }

                Utils.BuyCollection.AsParallel().ForAll(x =>
                {
                    if (arr.Any(y => y["i_classid"].ToString() == x.Classid)) return;
                    if (x.IsAutoBuy)
                    {
                        x.Status = true;
                        Task.Run(async () => { await InsertOrder.Insert(Utils.SecretKey, x); }).Wait();
                    }
                    else
                    {
                        x.Status = false;
                        x.CurrCost = 0;
                        x.Position = 0;
                    }
                });

                foreach (var x in arr)
                {
                    try
                    {
                        var first =
                            Utils.BuyCollection.First(
                                y =>
                                    y.Classid == x["i_classid"].ToString() &&
                                    y.Instanceid == x["i_instanceid"].ToString());
                        first.CurrCost = int.Parse(x["o_price"].ToString());
                        first.Status = true;
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