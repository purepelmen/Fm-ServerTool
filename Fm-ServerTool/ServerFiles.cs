using Fm_ServerTool.Model;
using Newtonsoft.Json;
using System.IO.Compression;

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

        public void SaveBuildInfo(GameBuild build)
        {
            File.WriteAllText(BuildInfoFile, JsonConvert.SerializeObject(build));
        }

        public void RemoveTemportaryFiles()
        {
            File.Delete(TempDownloadFile);
        }

        public string GetExecutablePath()
        {
            string executableName = GetBuildInfo().RunnableFile;
            return GameFolder + executableName;
        }

        public GameBuild GetBuildInfo()
        {
            GameBuild? gameBuild = JsonConvert.DeserializeObject<GameBuild>(File.ReadAllText(BuildInfoFile));
            if (gameBuild == null)
                throw new NullReferenceException();

            return gameBuild;
        }

        public void RemoveBuild()
        {
            Directory.Delete(GameFolder, true);
            File.Delete(BuildInfoFile);
        }

        public void DownloadBuild(string url)
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

        public void UnzipDownloadedBuild()
        {
            ZipFile.ExtractToDirectory(TempDownloadFile, GameFolder);
        }
    }
}
