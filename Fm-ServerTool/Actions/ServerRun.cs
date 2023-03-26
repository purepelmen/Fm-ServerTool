using System.Diagnostics;
using Fm_ServerTool.CommandArguments;
using Fm_ServerTool.Model;

namespace Fm_ServerTool.Actions
{
    public class ServerRun
    {
        public static void Handle(ArgumentParser argumentParser)
        {
            new ServerRun().Run();
        }

        public void Run()
        {
            ServerFiles server = new ServerFiles();
            if (server.IsInstalledAndValid == false)
            {
                Console.WriteLine("Server isn't installed or corrupted. Printing detailed information.");
                Console.Write(server.ToString());

                return;
            }

            RunExecutable(server.GetExecutableFilePath());
        }

        private void RunExecutable(string executablePath)
        {
            Process? process = Process.Start(new ProcessStartInfo(executablePath)
            {
                UseShellExecute = false
            });

            if (process == null)
            {
                Console.WriteLine("Failed to start game process.");
                return;
            }

            process.WaitForExit();
        }
    }
}