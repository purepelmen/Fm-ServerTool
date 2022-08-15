using Fm_ServerTool.Model;
using Newtonsoft.Json;

namespace Fm_ServerTool
{
    public static class WebDataUtils
    {
        public const string Url = "https://raw.githubusercontent.com/purepelmen/Fm-ServerTool/master/web-data/data.json";

        public static WebData Fetch()
        {
            Console.WriteLine("Fetching game web data...");

            string? result = NetUtils.TryDownloadString(Url, out string? errorMessage);
            if (result == null || errorMessage != null)
                throw new ProcedureFailureException($"Failed to fetch game web data: {errorMessage}");

            return JsonConvert.DeserializeObject<WebData>(result) ?? throw new NullReferenceException();
        }
    }
}
