﻿using Fm_ServerTool.Model;

namespace Fm_ServerTool
{
    public class ServerUpdate
    {
        private ArgumentParser _argumentParser;
        private ServerFiles _files;

        public ServerUpdate(ArgumentParser argumentParser)
        {
            _argumentParser = argumentParser;
            _files = new ServerFiles();

            Update();
        }

        private void Update()
        {
            if (_files.IsBuildInstalled() == false)
            {
                Console.WriteLine("Version isn't installed");
                return;
            }

            GameBuild installedBuild = _files.GetBuildInfo();
            WebData webData = WebDataUtils.Fetch();

            GameBuild newBuild = GetNewestBuild(webData, installedBuild.OperatingSystem);

            Console.WriteLine("Removing current version...");
            _files.RemoveBuild();

            Console.WriteLine($"Downloading last version ({newBuild.Name})...");
            _files.DownloadBuild(newBuild.Url);
            
            Console.WriteLine("Unzipping...");
            _files.UnzipDownloadedBuild();

            Console.WriteLine($"Saving build information...");
            _files.SaveBuildInfo(newBuild);

            Console.WriteLine($"Deleting temportary file...");
            _files.RemoveTemportaryFiles();
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