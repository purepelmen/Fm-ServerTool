using Fm_ServerTool.Model;
using Newtonsoft.Json;

namespace Fm_ServerTool
{
    public class ServerSetup
    {
        public const string WebDataUrl = "https://raw.githubusercontent.com/purepelmen/Fm-ServerTool/master/web-data/data.json";

        private ArgumentParser _argumentParser;

        public ServerSetup(ArgumentParser parser)
        {
            _argumentParser = parser;
            Setup();
        }

        private void Setup()
        {
            WebData webData = FetchWebData();
        }

        private WebData FetchWebData()
        {
            Console.WriteLine("Fetching game web data...");

            string? result = NetUtils.TryDownloadString(WebDataUrl, out string? errorMessage);
            if (result == null || errorMessage != null)
                throw new ProcedureFailureException($"Failed to fetch game web data: {errorMessage}");
            
            Console.WriteLine(result);
            return JsonConvert.DeserializeObject<WebData>(result) ?? throw new NullReferenceException();
        }
    }
}
