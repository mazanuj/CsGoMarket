using System;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.Utilities;

namespace CsGoMarketLib.API
{
    public static class SetPrice
    {
        public static async Task Set(string key, string uid, int price)
        {
            try
            {
                var unicodeString =
                    await
                        new WebClient().DownloadStringTaskAsync(
                            $"https://market.csgo.com/api/SetPrice/{uid}/{price}/?key={key}");
                var resp = await UnicodeConverter.Convert(unicodeString);
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
        }
    }
}