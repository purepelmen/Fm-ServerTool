# Fm-ServerTool
Facility Manager Server Tool for easy server managment.

This tool is better than directly downloading server builds because:
* By using the same command, you automatically download the latest server build.
* By using the same command, you can update to the latest build for your OS very easily.
* The tool handles downloading, unzipping, saving the config file and cleanup for you (and optionally auto-updating).
* In the future this tool may provide a tighter integration with the game which may provide many new interesting functions for the tool.

## Quick guide
What you only need to know is how to set everything up, then how to run and configure your server. This and some other useful operations are described here.

### Server Setup
1. Download the latest release of the tool to your server or PC.
2. Rename the file to something more convenient (for example "fm-tool").
3. Open command line in the folder where the downloaded file is located and enter this command:
```sh
fm-tool setup
```
4. The tool will ask you to select a version number to install. Select the version (selecting a version with an incompatible platform will not work).
5. Wait while the tool downloads and prepares the server build.
6. And then you're done.

### Server Running
```sh
fm-tool run
```


### Config editing
( ❗ ) - Note that the tool doesn't guarantees anything about the config location and other things related to it. It's up to a specific version of Facility Manager.

Before editing the config, the server must be installed and runned at least once.
When using a server build on Windows or Linux, the configuration file should be here (relative to the tool's path):
```
./FMST_Files/Facility Manager/Facility Manager_Data/StreamingAssets/server_config.cfg
```

### Server Updating
```sh
fm-tool update
```
This command will find latest version that is compatible with the installed one.

Also it's possible to enable auto-updates when running the server with this command (updates will be checking every 10 minutes):
```sh
fm-tool run -auto-update
```
