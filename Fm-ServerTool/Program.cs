using Fm_ServerTool.Actions;
using Fm_ServerTool.CommandArguments;

namespace Fm_ServerTool
{
    public class Program
    {
        public static readonly string Version;

        static Program()
        {
            Version currentAssemblyVersion = typeof(Program).Assembly.GetName().Version
                ?? throw new InvalidOperationException();

            Version = currentAssemblyVersion.ToString(3);
        }

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
            actionExecuter.ParsingErrorOccured += delegate (string errorMessage)
            {
                if (string.IsNullOrWhiteSpace(errorMessage) == false)
                {
                    Console.WriteLine(errorMessage);
                }

                errorCode = -1;
            };

            actionExecuter.Execute(args);
            return errorCode;
        }

        private static void RegisterAllActions(CommandActionExecuter actionExecuter)
        {
            actionExecuter.RegisterAction("setup", ServerSetup.Handle);
            actionExecuter.RegisterAction("update", ServerUpdate.Handle);
            actionExecuter.RegisterAction("erase", ServerErase.Handle);
            actionExecuter.RegisterAction("run", ServerRun.Handle);
        }
    }
}
