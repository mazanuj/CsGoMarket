using System;
using System.Linq;
using System.Threading.Tasks;
using CsGoMarketLib.API;
using CsGoMarketLib.Utilities;

namespace CsGoMarketLib.Sell
{
    public static class SellCycle
    {
        public static async Task ChangeSellItemPrice()
        {
            try
            {
                foreach (var y in Utils.SellCollection.Where(x => x.Status))
                {
                    if (!Utils.IsPermit) return;

                    var num = y.Position == 1 ? 1 : 0;
                    var prices = await GetItemPrice.GetOffers(y.Classid, y.Instanceid, Utils.SecretKey);

                    if (prices.Count == 1)
                    {
                        if (y.NormCost == y.CurrCost) continue;

                        await SetPrice.Set(Utils.SecretKey, y.Uid, y.NormCost);
                        Informer.RaiseOnResultReceived($"Set price {(y.LimitCost)/100}₽ for {y.Name}");

                        await Task.Delay(330);
                    }
                    else if (prices[num] <= y.LimitCost)
                    {
                        if (y.LimitCost == y.CurrCost) continue;

                        await SetPrice.Set(Utils.SecretKey, y.Uid, y.LimitCost);
                        Informer.RaiseOnResultReceived($"Set price {(y.LimitCost)/100}₽ for {y.Name}");

                        await Task.Delay(330);
                    }
                    else if (prices[num] - 1 >= y.LimitCost)
                    {
                        if (y.CurrCost == (prices[num] - 1)) continue;

                        await SetPrice.Set(Utils.SecretKey, y.Uid, prices[num] - 1);
                        Informer.RaiseOnResultReceived($"Set price {(prices[num] - 1)/100}₽ for {y.Name}");

                        await Task.Delay(330);
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