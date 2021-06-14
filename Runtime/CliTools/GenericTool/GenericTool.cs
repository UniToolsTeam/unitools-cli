namespace UniTools.CLI
{
    public sealed class GenericTool : BaseCliTool
        , ICliToolFriendlyName
    {
        private readonly CommandLine m_commandLine = default;

        public GenericTool(string executable, CommandLine commandLine)
        {
            Path = executable;
            m_commandLine = commandLine;
        }

        public string Name => nameof(GenericTool);

        public override string Path { get; }

        public override ToolResult Execute(string arguments = null, string workingDirectory = null)
        {
            if (string.IsNullOrEmpty(arguments))
            {
                arguments = string.Empty;
            }

            if (string.IsNullOrEmpty(workingDirectory))
            {
                workingDirectory = string.Empty;
            }

            return m_commandLine.Execute($"{Path} {arguments}", workingDirectory);
        }

        public override string ToString()
        {
            return $"{nameof(GenericTool)}: {Path}";
        }
    }
}