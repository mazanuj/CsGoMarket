using System;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.Utilities;

namespace CsGoMarketLib.API
{
    public static class SetPrice
    {
        public static async Task Set(string key)
        {
            try
            {
                var unicodeString =
                    await new WebClient().DownloadStringTaskAsync($"https://market.csgo.com/api/Trades/?key={key}");
                var resp = await UnicodeConverter.Convert(unicodeString);

                Informer.RaiseOnResultReceived(
                    $"Error: {resp.error}\r\nDescription: {resp.description}\r\nSuccess: {resp.success}\r\n{new string('=', 70)}{DateTime.Now.ToString("T")}");
            }
            catch (Exception ex)
            {
                Informer.RaiseOnResultReceived(ex);
            }
        }
    }
}