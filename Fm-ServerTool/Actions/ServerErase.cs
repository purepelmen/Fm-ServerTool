using Fm_ServerTool.CommandArguments;
using Fm_ServerTool.Model;

namespace Fm_ServerTool.Actions
{
    public class ServerErase : ICommandActionHandler
    {
        public void Handle(ArgumentParser argumentParser)
        {
            ServerFiles server = new ServerFiles();
            if (server.IsInstalled == false)
            {
                Console.WriteLine("Server isn't installed.");
                return;
            }

            Console.WriteLine($"Server files will be erased.");
            server.Uninstall();
        }
    }
}