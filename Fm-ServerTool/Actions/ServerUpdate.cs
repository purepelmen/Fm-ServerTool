using Fm_ServerTool.CommandArguments;
using Fm_ServerTool.Model;

namespace Fm_ServerTool.Actions
{
    public class ServerUpdate
    {
        public static void Handle(ArgumentParser argumentParser)
        {
            new ServerUpdate().Update();
        }

        public void Update()
        {
            ServerFiles server = new ServerFiles();
            if (server.IsInstalledAndValid == false)
            {
                Console.WriteLine("Server isn't installed or corrupted. Printing detailed information.");
                Console.Write(server.ToString());

                return;
            }

            GameBuild installedBuild = server.GetBuildInfo();
            WebData? webData = WebDataUtils.Fetch();
            if (webData == null) return;

            GameBuild? newBuild = GetNewestBuild(webData, installedBuild.OperatingSystem);
            if (newBuild == null)
            {
                Console.WriteLine("Can't found build for your operating system. Try to clear fm-servertool files.");
                return;
            }

            Console.WriteLine("\n=== Update Build Information ===");
            Console.WriteLine(newBuild);

            Console.WriteLine("Removing the current version...");
            server.Uninstall(true);
            server.Install(newBuild);
        }

        private GameBuild? GetNewestBuild(WebData webData, string operatingSystem)
        {
            foreach (GameBuild build in webData.LastBuilds)
            {
                if (build.OperatingSystem == operatingSystem)
                    return build;
            }

            return null;
        }
    }
}