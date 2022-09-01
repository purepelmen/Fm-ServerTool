namespace Fm_ServerTool.CommandArguments
{
    public class ProcedureFailureException : Exception
    {
        public ProcedureFailureException(string? message) : base(message)
        {
        }
    }
}