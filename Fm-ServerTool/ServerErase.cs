namespace Fm_ServerTool
{
    internal class ServerErase
    {
        private ArgumentParser _argumentParser;

        public ServerErase(ArgumentParser argumentParser)
        {
            _argumentParser = argumentParser;
            Erase();
        }

        private void Erase()
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