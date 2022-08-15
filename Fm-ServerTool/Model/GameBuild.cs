using Newtonsoft.Json;

namespace Fm_ServerTool.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GameBuild
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; private set; }

        [JsonProperty(PropertyName = "operating_system")]
        public string OperatingSystem { get; private set; }

        [JsonProperty(PropertyName = "run_file")]
        public string RunnableFile { get; private set; }

        public GameBuild(string name, string url, string operatingSystem, string runnableFile)
        {
            Name = name;
            Url = url;

            OperatingSystem = operatingSystem;
            RunnableFile = runnableFile;
        }
    }
}
