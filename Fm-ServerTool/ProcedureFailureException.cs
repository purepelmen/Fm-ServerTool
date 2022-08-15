
namespace Fm_ServerTool
{
    public class ProcedureFailureException : Exception
    {
        public ProcedureFailureException(string? message) : base(message)
        {
        }
    }
}