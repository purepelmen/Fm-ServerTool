using Fm_ServerTool.CommandArguments;
using Fm_ServerTool.Model;
using System.Runtime.InteropServices;

namespace Fm_ServerTool.Actions
{
    public class ServerSetup : ICommandActionHandler
    {
        public const string WindowsOs = "Windows";
        public const string LinuxOs = "Linux";

        public void Handle(ArgumentParser parser)
        {
            ServerFiles server = new ServerFiles();
            if (IsSetupNotDesired(server)) return;

            WebData? webData = WebDataUtils.Fetch();
            if (webData == null) return;

            Console.WriteLine($"Last game version is {webData.LastVersion}");

            GameBuild selectedBuild = AskForDesiredVersion(webData);
            Console.WriteLine("\n=== Selected Build Information ===");
            Console.Write(selectedBuild);

            server.Install(selectedBuild);
        }

        private bool IsSetupNotDesired(ServerFiles server)
        {
            if (server.IsInstalled)
            {
                Console.WriteLine("Do you really want to setup the server? It will destroy your previous server."
                    + "\nPress Y to continue. Any other key to cancel.");

                ConsoleKeyInfo key = Console.ReadKey(true);
                if (char.ToUpper(key.KeyChar) != 'Y')
                {
                    Console.WriteLine("Setup canceled.");
                    return true;
                }

                Console.WriteLine($"Server files will be erased.\n");
                server.Uninstall();
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
                bool readSuccess = TryReadInt(out int desiredVersion);
                if (readSuccess == false)
                {
                    Console.WriteLine("Invalid number. Try again.");
                    continue;
                }

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

        private bool TryReadInt(out int number)
        {
            string input = Console.ReadLine() ?? "0";
            input = input.Trim();

            try
            {
                number = Convert.ToInt32(input);
                return true;
            }
            catch (OverflowException)
            {
                number = 0;
                return false;
            }
            catch (FormatException)
            {
                number = 0;
                return false;
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
