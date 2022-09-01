using System.Diagnostics;
using Fm_ServerTool.CommandArguments;

namespace Fm_ServerTool.Actions
{
    public class ServerRun : ICommandActionHandler
    {
        private ServerFiles _files;

        public ServerRun()
        {
            _files = new ServerFiles();
        }

        public void Handle(ArgumentParser parser)
        {
            if (_files.IsBuildInstalled() == false)
            {
                Console.WriteLine("Server isn't installed.");
                return;
            }

            string executablePath = _files.GetExecutablePath();
            if (File.Exists(executablePath) == false)
            {
                Console.WriteLine("Server executable file not found. Try to reinstall the server.");
                return;
            }

            RunExecutable(executablePath);
        }

        private void RunExecutable(string executablePath)
        {
            Process? process = Process.Start(new ProcessStartInfo(executablePath)
            {
                UseShellExecute = false
            });

            if (process == null)
            {
                Console.WriteLine("Failed to start game process");
                return;
            }

            process.WaitForExit();
        }
    }
}