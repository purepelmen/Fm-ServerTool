using System.Text;
using System.Text.Json.Serialization;

namespace Fm_ServerTool.Model
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public class GameBuild
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("operating_system")]
        public string OperatingSystem { get; set; }

        [JsonPropertyName("run_file")]
        public string RunnableFile { get; set; }

        [JsonPropertyName("config_path")]
        public string? ConfigPath { get; set; }
        
        [JsonPropertyName("version_int")]
        public int VersionInt { get; set; }

        [JsonPropertyName("patches")]
        public string[]? Patches { get; set; }

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
