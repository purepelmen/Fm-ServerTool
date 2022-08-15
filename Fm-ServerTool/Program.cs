using Fm_ServerTool.Model;
using Newtonsoft.Json;

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

            ArgumentParser argumentParser = new ArgumentParser(args);

            try
            {
                argumentParser.Parse();
                return HandleAction(argumentParser);
            }
            catch (InvalidCommandArgumentException exception)
            {
                Console.WriteLine(exception.Message);
                return -1;
            }
        }

        private static int HandleAction(ArgumentParser argumentParser)
        {
            if (argumentParser.Action == null)
            {
                Console.WriteLine($"fm-servertool v{Version} is runned with no target action.");
                return 0;
            }

            Dictionary<string, Action<ArgumentParser>> actionMap = new Dictionary<string, Action<ArgumentParser>>()
            {
                { "setup", (argumentParser) => new ServerSetup(argumentParser) }
            };

            string action = argumentParser.Action;
            if (actionMap.TryGetValue(action, out Action<ArgumentParser>? actionHandler))
            {
                if (actionHandler == null)
                    throw new ArgumentNullException();

                actionHandler.Invoke(argumentParser);
                return 0;
            }

            Console.WriteLine($"fm-servertool v{Version}: unknown target action '{action}'");
            return -1;
        }
    }
}
