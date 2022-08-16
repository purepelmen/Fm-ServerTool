using Fm_ServerTool.Model;
using System.Runtime.InteropServices;

namespace Fm_ServerTool
{
    public class ServerSetup
    {
        public const string WindowsOs = "Windows";
        public const string LinuxOs = "Linux";

        private ArgumentParser _argumentParser;
        private ServerFiles _files;

        public ServerSetup(ArgumentParser parser)
        {
            _argumentParser = parser;
            _files = new ServerFiles();

            Setup();
        }

        private void Setup()
        {
            if (IsSetupNotDesired()) return;
            WebData webData = WebDataUtils.Fetch();

            Console.WriteLine($"Last game version is {webData.LastVersion}");
            GameBuild selectedBuild = AskForDesiredVersion(webData);

            Console.WriteLine($"\nDownloading {selectedBuild.Name}...");
            _files.DownloadBuild(selectedBuild.Url);

            Console.WriteLine($"Unzipping...");
            _files.UnzipDownloadedBuild();

            Console.WriteLine($"Saving build information...");
            _files.SaveBuildInfo(selectedBuild);

            Console.WriteLine($"Deleting temportary file...");
            _files.RemoveTemportaryFiles();
        }

        private bool IsSetupNotDesired()
        {
            if (_files.IsBuildInstalled())
            {
                Console.WriteLine("Do you really want to setup the server? It will destroy your previous server."
                    + "\nPress Y to continue. Any other key to cancel.");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.Write("\n\n");

                if (char.ToUpper(key.KeyChar) != 'Y')
                {
                    Console.WriteLine("Setup canceled.");
                    return true;
                }

                Console.WriteLine($"Erasing server files...");
                _files.Erase();
            }

            return false;
        }

        private GameBuild AskForDesiredVersion(WebData webData)
        {
            Console.WriteLine("\nType number of the version you want to install:");

            int i = 0;
            foreach (GameBuild gameBuild in webData.LastBuilds)
            {
                Console.WriteLine($"{i++} - {gameBuild.Name} (for {gameBuild.OperatingSystem})");
            }

            Console.Write('\n');
            while (true)
            {
                string versionInput = Console.ReadLine() ?? "0";
                versionInput = versionInput.Trim();

                if (versionInput == "")
                    versionInput = "0";

                int desiredVersion = Convert.ToInt32(versionInput);
                if (desiredVersion < 0 || desiredVersion >= webData.LastBuilds.Length)
                {
                    Console.WriteLine("Selected number < 0 or too big for version index. Try again.");
                    continue;
                }

                if (webData.LastBuilds[desiredVersion].OperatingSystem != GetCurrentOs())
                {
                    Console.WriteLine("Selected version isn't compatible with you OS. Select another.");
                    continue;
                }

                return webData.LastBuilds[desiredVersion];
            }
        }

        private string GetCurrentOs()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return WindowsOs;

            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return LinuxOs;

            return "Unknown";
        }
    }
}
