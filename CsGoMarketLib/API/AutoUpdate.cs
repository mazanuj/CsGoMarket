using System.Net;
using System.Threading.Tasks;

namespace CsGoMarketLib.API
{
    public static class AutoUpdate
    {
        public static async Task Update(string key)
        {
            await new WebClient().DownloadStringTaskAsync($"https://market.csgo.com/api/PingPong/?key={key}");
        }
    }
}