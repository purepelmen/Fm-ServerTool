namespace Fm_ServerTool.Actions
{
    public class ServerErase : ICommandActionHandler
    {
        public void Handle(ArgumentParser argumentParser)
        {
            ServerFiles files = new ServerFiles();
            if (files.IsBuildInstalled() == false)
            {
                Console.WriteLine("Server build isn't installed.");
                return;
            }

            Console.WriteLine("Erasing...");
            files.Erase();
        }
    }
}