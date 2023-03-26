namespace Fm_ServerTool.CommandArguments
{
    public class CommandActionExecuter
    {
        public event Action<string>? UnregistredActionErrorOccured;
        public event Action<string>? ParsingErrorOccured;
        public event Action? NoTargetActionErrorOccured;

        private Dictionary<string, Action<ArgumentParser>> _actionMap;

        public CommandActionExecuter()
        {
            _actionMap = new Dictionary<string, Action<ArgumentParser>>();
        }

        public void RegisterAction(string action, Action<ArgumentParser> handler)
        {
            _actionMap.Add(action, handler);
        }

        public void Execute(string[] args)
        {
            ArgumentParser argumentParser = new ArgumentParser(args);

            try
            {
                argumentParser.Parse();
                HandleAction(argumentParser);
            }
            catch (ArgumentParsingException exception)
            {
                ParsingErrorOccured?.Invoke(exception.Message);
            }
        }

        private void HandleAction(ArgumentParser argumentParser)
        {
            if (argumentParser.Action == null)
            {
                NoTargetActionErrorOccured?.Invoke();
                return;
            }

            string action = argumentParser.Action;
            if (_actionMap.TryGetValue(action, out Action<ArgumentParser>? actionHandler))
            {
                if (actionHandler == null)
                    throw new ArgumentNullException();

                actionHandler.Invoke(argumentParser);
                return;
            }

            UnregistredActionErrorOccured?.Invoke(action);
        }
    }
}
