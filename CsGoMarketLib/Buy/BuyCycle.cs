using System;
using System.Linq;
using System.Threading.Tasks;
using CsGoMarketLib.API;
using CsGoMarketLib.Utilities;

namespace CsGoMarketLib.Buy
{
    public static class BuyCycle
    {
        public static async Task ChangeBuyItemPrice()
        {
            try
            {
                foreach (var y in Utils.BuyCollection.Where(x => x.Status))
                {
                    if (!Utils.IsPermit) return;

                    var prices = await GetItemPrice.GetBuyOffers(y.Classid, y.Instanceid, Utils.SecretKey);
                    var num = y.CurrCost == prices.First() ? 1 : 0;

                    if (prices.Count == 1)
                    {
                        if (y.NormCost == y.CurrCost) continue;

                        await UpdateOrder.Update(Utils.SecretKey, y, y.NormCost);

                        await Task.Delay(500);
                    }
                    else if (prices[num] >= y.LimitCost)
                    {
                        if (y.LimitCost == y.CurrCost) continue;

                        await UpdateOrder.Update(Utils.SecretKey, y, y.LimitCost);
                        await Task.Delay(500);
                    }
                    else if (prices[num] + 1 <= y.LimitCost)
                    {
                        if (y.CurrCost == (prices[num] + 1)) continue;

                        await UpdateOrder.Update(Utils.SecretKey, y, prices[num] + 1);
                        await Task.Delay(500);
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