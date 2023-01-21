using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Fm_ServerTool.Model
{
    public class ServerFiles
    {
        public enum Validating
        {
            NotInstalled,
            MissingBuildInfo,
            CorruptedBuildInfo,
            MissingRunnableFile,
            Installed,
        }

        public const string TempDownloadFile = BaseFolder + "download.zip";
        public const string BuildInfoFile = BaseFolder + "build_info.json";

        public const string GameFolder = BaseFolder + "Facility Manager/";
        public const string BaseFolder = "FMST_Files/";

        public bool IsInstalledAndValid => State == Validating.Installed;
        public bool IsInstalled => State != Validating.NotInstalled;
        public string ErrorDetails => _errorBuffer.ToString();
        public Validating State { get; private set; }

        private StringBuilder _errorBuffer;

        private string? _executableFilePath;
        private GameBuild? _gameBuild;

        public ServerFiles()
        {
            _errorBuffer = new StringBuilder();
            Validate();
        }

        public string GetExecutableFilePath()
        {
            if (_executableFilePath == null)
                throw new NullReferenceException("Attempt to get executable file path when server isn't installed or corrupted.");

            return _executableFilePath;
        }

        public GameBuild GetBuildInfo()
        {
            if (_gameBuild == null)
                throw new NullReferenceException("Attempt to get build info when server isn't installed or corrupted.");

            return _gameBuild;
        }

        public void Uninstall()
        {
            if (IsInstalled == false)
                throw new InvalidOperationException("Server wasn't installed");

            State = Validating.NotInstalled;
            Directory.Delete(BaseFolder, true);
        }

        public bool Install(GameBuild build)
        {
            if (IsInstalled)
                throw new InvalidOperationException("Server is installed. Before installing uninstal it.");
            
            Directory.CreateDirectory(BaseFolder);

            Console.WriteLine($"\n[1/4] Downloading {build.Name}...");
            if (!BuildDownloader.Download(TempDownloadFile, build.Url))
            {
                return false;
            }

            Console.WriteLine($"[2/4] Unzipping...");
            ZipFile.ExtractToDirectory(TempDownloadFile, GameFolder);

            Console.WriteLine($"[3/4] Saving build information...");
            File.WriteAllText(BuildInfoFile, JsonSerializer.Serialize(build));

            Console.WriteLine($"[4/4] Removing temportary file...");
            File.Delete(TempDownloadFile);

            Validate();
            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Server validating state = ").Append(State).AppendLine();

            string errorDetails = ErrorDetails;
            if (string.IsNullOrEmpty(errorDetails) == false)
            {
                builder.AppendLine();
                builder.Append(errorDetails);
            }

            return builder.ToString();
        }

        private void Validate()
        {
            State = Validating.NotInstalled;
            _errorBuffer.Clear();

            if (Directory.Exists(BaseFolder) == false)
                return;

            if (File.Exists(BuildInfoFile) == false)
            {
                State = Validating.MissingBuildInfo;
                return;
            }

            string jsonSource = File.ReadAllText(BuildInfoFile);
            try
            {
                _gameBuild = JsonSerializer.Deserialize<GameBuild>(jsonSource);
            }
            catch (JsonException exception)
            {
                _errorBuffer.AppendLine($"Failed to parse JSON from {BuildInfoFile} file (maybe file is corrupted).");
                _errorBuffer.AppendLine($"Error on {exception.LineNumber} line with details: {exception.Message}");

                State = Validating.CorruptedBuildInfo;
                return;
            }

            string runnableFile = GetBuildInfo().RunnableFile;
            if (string.IsNullOrWhiteSpace(runnableFile))
            {
                State = Validating.CorruptedBuildInfo;
                return;
            }

            _executableFilePath = GameFolder + runnableFile;
            if (File.Exists(_executableFilePath) == false)
            {
                State = Validating.MissingRunnableFile;
                return;
            }

            State = Validating.Installed;
        }
    }
}
