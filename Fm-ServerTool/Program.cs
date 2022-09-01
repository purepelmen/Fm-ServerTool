﻿using Fm_ServerTool.Actions;
using Fm_ServerTool.CommandArguments;

namespace Fm_ServerTool
{
    public class Program
    {
        public static readonly string Version = "1.0";

        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"fm-servertool v{Version} is runned with no arguments.");
                return 0;
            }

            CommandActionExecuter actionExecuter = new CommandActionExecuter();
            RegisterAllActions(actionExecuter);

            int errorCode = 0;
            actionExecuter.NoTargetActionErrorOccured += delegate
            {
                Console.WriteLine($"fm-servertool v{Version} is runned with no target action.");
            };
            actionExecuter.UnregistredActionErrorOccured += delegate (string action)
            {
                Console.WriteLine($"fm-servertool v{Version}: unknown target action '{action}'");
                errorCode = -1;
            };
            actionExecuter.ParsingOrProcedureFailureOccured += delegate
            {
                errorCode = -1;
            };

            actionExecuter.Execute(args);
            return errorCode;
        }

        private static void RegisterAllActions(CommandActionExecuter actionExecuter)
        {
            actionExecuter.RegisterAction("setup", new ServerSetup());
            actionExecuter.RegisterAction("update", new ServerUpdate());
            actionExecuter.RegisterAction("erase", new ServerErase());
            actionExecuter.RegisterAction("run", new ServerRun());
        }
    }
}
