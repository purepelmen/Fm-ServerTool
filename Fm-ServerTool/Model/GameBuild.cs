using System.Text;
using System.Text.Json.Serialization;

namespace Fm_ServerTool.Model
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    public class GameBuild
    {
        [JsonPropertyName("name")]
        public string Name { get; init; }

        [JsonPropertyName("url")]
        public string Url { get; init; }

        [JsonPropertyName("operating_system")]
        public string OperatingSystem { get; init; }

        [JsonPropertyName("run_file")]
        public string RunnableFile { get; init; }

        [JsonPropertyName("config_path")]
        public string? ConfigPath { get; init; }
        
        [JsonPropertyName("version_int")]
        public int VersionInt { get; init; }

        [JsonPropertyName("patches")]
        public string[]? Patches { get; init; }

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
