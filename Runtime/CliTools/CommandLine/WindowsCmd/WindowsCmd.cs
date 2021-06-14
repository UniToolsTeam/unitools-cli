using System.Diagnostics;

namespace UniTools.CLI
{
    public sealed class WindowsCmd : CommandLine
        , ICliToolVersion
        , ICliToolFriendlyName
    {
        private string m_version = string.Empty;

        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(m_version))
                {
                    m_version = Execute("ver").Output;
                }

                return m_version;
            }
        }

        public override string Path => "cmd.exe";
        public string Name => nameof(WindowsCmd);

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

            string escapedArgs = arguments.Replace("\"", "\\\"");
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = workingDirectory,
                    FileName = Path,
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
            int exitCode = process.ExitCode;

            return new ToolResult(
                output,
                error,
                exitCode);
        }
    }
}