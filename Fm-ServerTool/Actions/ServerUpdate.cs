using Fm_ServerTool.CommandArguments;
using Fm_ServerTool.Model;

namespace Fm_ServerTool.Actions
{
    public class ServerUpdate : ICommandActionHandler
    {
        public void Handle(ArgumentParser argumentParser)
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

            GameBuild newBuild = GetNewestBuild(webData, installedBuild.OperatingSystem);
            Console.WriteLine("\n=== Update Build Information ===");
            Console.WriteLine(newBuild);

            Console.WriteLine("Removing the current version...");
            server.Uninstall();
            server.Install(newBuild);
        }

        private GameBuild GetNewestBuild(WebData webData, string operatingSystem)
        {
            foreach (GameBuild build in webData.LastBuilds)
            {
                if (build.OperatingSystem == operatingSystem)
                    return build;
            }

            throw new ProcedureFailureException("Can't found build for your operating system. Try to clear fm-servertool files.");
        }
    }
}