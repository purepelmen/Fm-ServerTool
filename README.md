# Fm-ServerTool
Facility Manager Server Tool for easy server managment.

## Server Setup
1. Download the latest release of the tool to your server or PC.
2. Rename the file to something more convenient (for example "fm-tool").
3. Open command line in the folder where the downloaded file is located and enter this command:
```sh
fm-tool setup
```
4. The tool will ask you to select a version number to install. Select the version (selecting a version with an incompatible platform will not work).
5. Wait while the tool downloads and prepares the server build.
6. And then you're done.

## Config editing
Before config editing the server must be installed and runned at least once.
When using server build (with Fm-ServerTool or not) on Windows or Linux, the configuration file can be found here:
```
{the tool file location}/FMST_Files/Facility Manager/Facility Manager_Data/StreamingAssets/server_config.cfg
```

## Server Running
```sh
fm-tool run
```

## Server Updating
```sh
fm-tool update
```
This command will find latest version that is compatible with the installed one.

Also it's possible to enable auto-updates with this command (updates will be checking every 10 minutes):
```sh
fm-tool update -auto-update
```
