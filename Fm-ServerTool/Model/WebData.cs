using Newtonsoft.Json;

namespace Fm_ServerTool.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WebData
    {
        [JsonProperty(PropertyName = "last_builds")]
        public GameBuild[] LastBuilds { get; private set; }

        [JsonProperty(PropertyName = "last_version")]
        public string LastVersion { get; private set; }

        public WebData(string lastVersion, GameBuild[] lastBuilds)
        {
            LastBuilds = lastBuilds;
            LastVersion = lastVersion;
        }
    }
}
