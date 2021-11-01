namespace UniTools.CLI
{
    public abstract class PathResolver : BaseCliTool
    {
        internal static readonly PathResolver Default =
#if UNITY_EDITOR_OSX
            new OsxWhich(CommandLine.Default);

#elif UNITY_EDITOR_WIN
            new WindowsWhere(CommandLine.Default);
#else
//TODO Create Unsupported tool!
        default;
#endif

        protected readonly CommandLine CommandLine = default;

        protected PathResolver(CommandLine commandLine)
        {
            CommandLine = commandLine;
        }
    }
}