using Fm_ServerTool.CommandArguments;
using Fm_ServerTool.Model;

namespace Fm_ServerTool.Actions
{
    public class ServerUpdate : ICommandActionHandler
    {
        private ServerFiles _files;

        public ServerUpdate()
        {
            _files = new ServerFiles();
        }

        public void Handle(ArgumentParser argumentParser)
        {
            if (_files.IsBuildInstalled() == false)
            {
                Console.WriteLine("Server isn't installed");
                return;
            }

            GameBuild installedBuild = _files.GetBuildInfo();
            WebData webData = WebDataUtils.Fetch();

            GameBuild newBuild = GetNewestBuild(webData, installedBuild.OperatingSystem);
            Console.WriteLine("\n=== Update Build Information ===");
            Console.WriteLine(newBuild);

            Console.WriteLine("Removing the current version...");
            _files.RemoveBuild();
            _files.InstallAndPrepare(newBuild);
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