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

            GameBuild? newBuild = TryFindNewestBuild(webData, installedBuild);
            if (newBuild == null)
            {
                Console.WriteLine("No update is available with OS specified in the installed build.");
                return;
            }

            Update(server, newBuild);
        }

        public void Update(ServerFiles server, GameBuild newBuild)
        {
            Console.WriteLine("\n=== Update Build Information ===");
            Console.WriteLine(newBuild);

            Console.WriteLine("Removing the current version...");
            server.Uninstall(true);
            server.Install(newBuild);
        }

        public GameBuild? TryFindNewestBuild(WebData webData, GameBuild currentBuild)
        {
            var sortedFiltered = from build in webData.LastBuilds
                                 where build.OperatingSystem == currentBuild.OperatingSystem
                                 orderby build.VersionInt descending
                                 select build;

            GameBuild? gameBuild = sortedFiltered.FirstOrDefault();
            if (gameBuild != null && gameBuild.VersionInt > currentBuild.VersionInt)
            {
                return gameBuild;
            }

            return null;
        }
    }
}