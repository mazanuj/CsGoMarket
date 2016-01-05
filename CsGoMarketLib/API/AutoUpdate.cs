using System;
using System.Net;
using System.Threading.Tasks;
using CsGoMarketLib.Utilities;

namespace CsGoMarketLib.API
{
    public static class AutoUpdate
    {
        public static async Task Update(string key)
        {
            var unicodeString =
                await new WebClient().DownloadStringTaskAsync($"https://market.csgo.com/api/PingPong/?key={key}");
            var resp = await UnicodeConverter.Convert(unicodeString);

            Informer.RaiseOnResultReceived(
                $"Error: {resp.error}\r\nDescription: {resp.description}\r\nSuccess: {resp.success}\r\n{new string('=', 70)}{DateTime.Now.ToString("T")}");
        }
    }
}