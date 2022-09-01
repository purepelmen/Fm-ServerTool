using Fm_ServerTool.CommandArguments;

namespace Fm_ServerTool.Actions
{
    public class ServerErase : ICommandActionHandler
    {
        public void Handle(ArgumentParser argumentParser)
        {
            ServerFiles files = new ServerFiles();
            if (files.IsBuildInstalled() == false)
            {
                Console.WriteLine("Server isn't installed.");
                return;
            }

            Console.WriteLine($"Server files will be erased.");
            files.Erase();
        }
    }
}