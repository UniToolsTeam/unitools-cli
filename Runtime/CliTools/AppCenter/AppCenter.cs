using UniTools.CLI;

[assembly: AppCenter]

namespace UniTools.CLI
{
    public sealed class AppCenter : BaseCliTool
        , ICliToolVersion
        , ICliToolFriendlyName
        , ICliToolHelpLink
    {
        public const string ToolName = "appcenter";

        private readonly CommandLine m_commandLine = default;
        private string m_version = string.Empty;

        public AppCenter(string path, CommandLine commandLine)
        {
            Path = path;
            m_commandLine = commandLine;
        }

        public string Name => nameof(AppCenter);
        public string Link => "https://docs.microsoft.com/en-us/appcenter/cli/";

        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(m_version))
                {
                    m_version = Execute("--version").Output;
                }

                return m_version;
            }
        }

        public override string Path { get; } = default;

        public override ToolResult Execute(string arguments = null, string workingDirectory = null)
        {
            if (!IsInstalled)
            {
                throw new ToolNotInstalledException();
            }
            
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
            return $"{nameof(AppCenter)}: {Path}, {Version}";
        }
    }
}