using Fm_ServerTool.Model;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Fm_ServerTool
{
    public class ServerFiles
    {
        public const string TempDownloadFile = ServerFilesFolder + "download.zip";
        public const string BuildInfoFile = ServerFilesFolder + "build_info.json";

        public const string GameFolder = ServerFilesFolder + "Facility Manager/";
        public const string ServerFilesFolder = "FMST_Files/";

        public bool IsBuildInstalled()
        {
            return File.Exists(BuildInfoFile);
        }

        public void Erase()
        {
            Directory.Delete(ServerFilesFolder, true);
        }

        public GameBuild GetBuildInfo()
        {
            GameBuild? gameBuild = JsonSerializer.Deserialize<GameBuild>(File.ReadAllText(BuildInfoFile));
            if (gameBuild == null)
                throw new NullReferenceException();

            return gameBuild;
        }

        public string GetExecutablePath()
        {
            string executableName = GetBuildInfo().RunnableFile;
            return GameFolder + executableName;
        }

        public void SaveBuildInfo(GameBuild build)
        {
            File.WriteAllText(BuildInfoFile, JsonSerializer.Serialize(build));
        }

        public void InstallAndPrepare(GameBuild build)
        {
            Console.WriteLine($"\n[1/4] Downloading {build.Name}...");
            DownloadBuild(build.Url);

            Console.WriteLine($"[2/4] Unzipping...");
            ZipFile.ExtractToDirectory(TempDownloadFile, GameFolder);

            Console.WriteLine($"[3/4] Saving build information...");
            SaveBuildInfo(build);

            Console.WriteLine($"[4/4] Removing temportary file...");
            File.Delete(TempDownloadFile);
        }

        public void RemoveBuild()
        {
            Directory.Delete(GameFolder, true);
            File.Delete(BuildInfoFile);
        }

        private void DownloadBuild(string url)
        {
            Directory.CreateDirectory(ServerFilesFolder);

            using (HttpClient client = new HttpClient())
            {
                Stream stream = client.GetStreamAsync(url).Result;
                using (FileStream fileStream = new FileStream(TempDownloadFile, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }
            }
        }
    }
}
