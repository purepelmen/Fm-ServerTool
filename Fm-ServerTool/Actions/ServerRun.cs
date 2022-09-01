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
                Console.WriteLine("Server build isn't installed.");
                return;
            }

            string executablePath = _files.GetExecutablePath();
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