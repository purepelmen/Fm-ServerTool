namespace Fm_ServerTool.CommandArguments
{
    public interface ICommandActionHandler
    {
        public void Handle(ArgumentParser argumentParser);
    }
}
