using System.Text.Json.Serialization;

namespace Fm_ServerTool.Model
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public class WebData
    {
        [JsonPropertyName("last_builds")]
        public GameBuild[] LastBuilds { get; private set; }

        [JsonPropertyName("last_version")]
        public string LastVersion { get; private set; }

        public WebData(string lastVersion, GameBuild[] lastBuilds)
        {
            LastBuilds = lastBuilds;
            LastVersion = lastVersion;
        }
    }
}
