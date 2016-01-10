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

                    var first = Utils.BuyCollection.First(x => x.Status);
                    var prices = await GetItemPrice.GetBuyOffers(first.Classid, first.Instanceid, Utils.SecretKey);
                }
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
        }
    }
}