namespace Fm_ServerTool
{
    public class ArgumentParser
    {
        public string? Action { get; private set; }

        private readonly Dictionary<string, string> _arguments;
        private readonly string[] _args;

        private int _argIndex;

        public ArgumentParser(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("No arguments");

            _args = args;
            _arguments = new Dictionary<string, string>();
        }

        public bool TryGetArgument(string key, out string value)
        {
            if (_arguments.TryGetValue(key, out string keyValue))
            {
                value = keyValue;
                return true;
            }

            value = "";
            return false;
        }

        public string GetArgument(string key)
        {
            return _arguments[key];
        }

        public void Parse()
        {
            _arguments.Clear();
            _argIndex = 0;

            if (_args[_argIndex].StartsWith("-") == false)
            {
                Action = _args[_argIndex++];
            }

            while (HasNotParsedArguments())
            {
                ParseNext();
            }
        }

        private void ParseNext()
        {
            string argKey = _args[_argIndex++];

            if (argKey.StartsWith("--"))
            {
                if (HasNotParsedArguments() == false)
                    throw new InvalidCommandArgumentException($"Argument {argKey} must have value");

                string argValue = _args[_argIndex++];
                AddArgument(argKey, argValue);

                return;
            }

            throw new InvalidCommandArgumentException("Required argument started with -- or flag started with -");
        }

        private void AddArgument(string argKey, string argValue)
        {
            if (_arguments.ContainsKey(argKey))
                throw new InvalidCommandArgumentException($"Found second '{argKey}' argument declaration");

            _arguments.Add(argKey, argValue);
        }

        private bool HasNotParsedArguments()
        {
            return _argIndex < _args.Length;
        }
    }
}
