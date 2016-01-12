using System.Linq;
using System.Threading.Tasks;
using CsGoMarketLib.Utilities;

namespace CsGoMarketLib.API
{
    public static class RemoveAllOrders
    {
        public static async Task Remove(string key)
        {
            foreach (var y in Utils.BuyCollection.Where(x => x.IsAutoBuy || x.Status))
            {
                await UpdateOrder.Update(key, y, 0);
                y.IsAutoBuy = false;
                y.Status = false;
                y.Position = 0;
                y.CurrCost = 0;
            }
        }
    }
}