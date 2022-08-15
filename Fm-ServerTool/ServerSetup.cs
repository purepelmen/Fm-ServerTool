using Fm_ServerTool.Model;
using Newtonsoft.Json;

namespace Fm_ServerTool
{
    public class ServerSetup
    {
        public const string DataUrl = "";

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
            using (HttpClient client = new HttpClient())
            {
                string result = client.GetStringAsync(DataUrl).Result;
                Console.WriteLine(result);

                return JsonConvert.DeserializeObject<WebData>(result) ?? throw new NullReferenceException();
            }
        }
    }
}
