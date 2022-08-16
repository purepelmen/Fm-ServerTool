using System.Diagnostics;

namespace Fm_ServerTool
{
    internal class ServerRun
    {
        private ArgumentParser _argumentParser;
        private ServerFiles _files;

        public ServerRun(ArgumentParser argumentParser)
        {
            _argumentParser = argumentParser;
            _files = new ServerFiles();

            Run();
        }

        private void Run()
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