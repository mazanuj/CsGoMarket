using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using xNet.Net;

namespace CsGoMarketLib.API
{
    public static class DownloadChart
    {
        public static async Task Download(string classid_instanceid)
        {
            await Task.Run(() =>
            {
                var req = new HttpRequest();
                var doc = new HtmlDocument();
                var instance = classid_instanceid.Replace("_", "-");

                var resp = req.Get($"https://market.csgo.com/item/{instance}").ToString();
                doc.LoadHtml(resp);
                var svg = doc.DocumentNode.Descendants("svg").First();

                req.AddField("filename", "chart");
                req.AddField("type", "image/jpeg");
                req.AddField("width", "0");
                req.AddField("scale", "2");
                req.AddField("svg", svg.InnerHtml);

                req.Post("http://export.highcharts.com/").ToFile("123.jpg");
            });
        }
    }
}