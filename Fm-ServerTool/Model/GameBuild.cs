using System.Text;
using System.Text.Json.Serialization;

namespace Fm_ServerTool.Model
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public class GameBuild
    {
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        [JsonPropertyName("url")]
        public string Url { get; private set; }

        [JsonPropertyName("operating_system")]
        public string OperatingSystem { get; private set; }

        [JsonPropertyName("run_file")]
        public string RunnableFile { get; private set; }

        [JsonPropertyName("config_path")]
        public string ConfigPath { get; private set; }
        
        [JsonPropertyName("version_int")]
        public int VersionInt { get; private set; }

        public GameBuild(string name, string url, string operatingSystem, string runnableFile, string configPath, int versionInt)
        {
            Name = name;
            Url = url;
            OperatingSystem = operatingSystem;
            RunnableFile = runnableFile;
            ConfigPath = configPath;
            VersionInt = versionInt;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Build Name = ").AppendLine(Name);
            builder.Append("Compatible OS = ").AppendLine(OperatingSystem);
            builder.Append("Runnable File = ").AppendLine(RunnableFile);

            return builder.ToString();
        }
    }
}
