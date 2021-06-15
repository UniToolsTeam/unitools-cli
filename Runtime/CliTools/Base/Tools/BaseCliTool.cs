namespace UniTools.CLI
{
    public abstract class BaseCliTool :
        ICliToolPath
        , ICliToolInstalled
    {
        public virtual bool IsInstalled => !string.IsNullOrEmpty(Path);
        public abstract string Path { get; }
        public abstract ToolResult Execute(string arguments = null, string workingDirectory = null);
    }
}