namespace Fm_ServerTool.CommandArguments
{
    public class ArgumentParsingException : Exception
    {
        public ArgumentParsingException()
        {
        }

        public ArgumentParsingException(string? message) : base(message)
        {
        }

        public ArgumentParsingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}