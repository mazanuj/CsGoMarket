using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CsGoMarketLib.API
{
    public static class UnicodeConverter
    {
        public static async Task<dynamic> Convert(string unicodeString)
        {
            return await Task.Run(() =>
            {
                // Create two different encodings.
                var ascii = Encoding.UTF8;
                var unicode = Encoding.Unicode;

                // Convert the string into a byte array.
                var unicodeBytes = unicode.GetBytes(unicodeString);

                // Perform the conversion from one encoding to the other.
                var asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

                // Convert the new byte[] into a char[] and then into a string.
                var asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                var asciiString = new string(asciiChars);

                dynamic json = JObject.Parse(asciiString);
                return json;
            });
        }
    }
}