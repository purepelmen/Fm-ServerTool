﻿namespace Fm_ServerTool.CommandArguments
{
    public class InvalidCommandArgumentException : Exception
    {
        public InvalidCommandArgumentException()
        {
        }

        public InvalidCommandArgumentException(string? message) : base(message)
        {
        }

        public InvalidCommandArgumentException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}