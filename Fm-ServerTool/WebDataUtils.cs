using Fm_ServerTool.Model;
using System.Text.Json;

namespace Fm_ServerTool
{
    public static class WebDataUtils
    {
        public const string Url = "https://raw.githubusercontent.com/purepelmen/Fm-ServerTool/master/web-data/data.json";

        public static WebData? Fetch()
        {
            Console.WriteLine("[WebDataUtils] Fetching up-to-date data...");

            string? result = GetString();
            if (result == null)
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<WebData>(result);
            }
            catch (JsonException exception)
            {
                Console.WriteLine("[WebDataUtils] Failed to parse JSON from the received web data.");
                Console.WriteLine($"Error on {exception.LineNumber} line with details: {exception.Message}");

                return null;
            }
        }

        private static string? GetString()
        {
            HttpClient client = new HttpClient();
            string? result = null;

            try
            {
                result = client.GetStringAsync(Url).Result;
            }
            catch (AggregateException exception)
            {
                Console.WriteLine($"[WebDataUtils] Failed to fetch web data: {exception.InnerException?.Message}");
            }
            finally
            {
                client.Dispose();
            }

            return result;
        }
    }
}
