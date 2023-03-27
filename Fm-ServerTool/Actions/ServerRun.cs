using System.Diagnostics;
using Fm_ServerTool.CommandArguments;
using Fm_ServerTool.Model;

namespace Fm_ServerTool.Actions
{
    public class ServerRun
    {
        private readonly bool _autoUpdateEnabled;

        public static void Handle(ArgumentParser argumentParser)
        {
            new ServerRun(argumentParser.HasDefinedFlag("-auto-update")).Run();
        }

        public ServerRun(bool autoUpdateEnabled)
        {
            _autoUpdateEnabled = autoUpdateEnabled;
        }

        public void Run()
        {
            ServerFiles files = new ServerFiles();
            if (files.IsInstalledAndValid == false)
            {
                Console.WriteLine("Server isn't installed or corrupted. Printing detailed information.");
                Console.Write(files.ToString());

                return;
            }

            ServerUpdate serverUpdate = new ServerUpdate();
            Process? process = null;

            int delay = 1000 * 10;
            while (true)
            {
                if (process == null)
                {
                    process = RunExecutable(files.GetBuildInfo(), files.GetExecutableFilePath());
                    if (process == null) return;
                }

                if (!_autoUpdateEnabled)
                {
                    while (true) { Thread.Sleep(1000 * 60); }
                }

                Thread.Sleep(delay);

                Console.WriteLine("[Auto-update] Checking...");
                GameBuild? newestBuild = TryFindUpdate(serverUpdate, files.GetBuildInfo());
                if (newestBuild != null)
                {
                    Console.WriteLine("[Auto-update] Found new build. Updating...");
                    process.Kill();
                    serverUpdate.Update(files, newestBuild);

                    // Re-init to analyze and reload data from disk
                    files = new ServerFiles();
                    // Will re-initialize the process
                    process = null;
                }
            }
        }

        private Process? RunExecutable(GameBuild gameBuild, string executablePath)
        {
            Process? process = Process.Start(new ProcessStartInfo(executablePath)
            {
                UseShellExecute = false
            });

            if (process == null)
            {
                Console.WriteLine("Failed to start game process.");
                return null;
            }
            
            return process;
        }

        private GameBuild? TryFindUpdate(ServerUpdate serverUpdate, GameBuild gameBuild)
        {
            WebData? webData = WebDataUtils.Fetch();
            if (webData == null) return null;

            GameBuild? newestBuild = serverUpdate.TryFindNewestBuild(webData, gameBuild);
            if (newestBuild == null) return null;

            return newestBuild;
        }
    }
}