using Fm_ServerTool.CommandArguments;
using Fm_ServerTool.Model;

namespace Fm_ServerTool.Actions
{
    public class ServerErase
    {
        public static void Handle(ArgumentParser argumentParser)
        {
            new ServerErase().Erase();
        }

        public void Erase()
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