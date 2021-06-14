namespace UniTools.CLI
{
    public sealed class ToolResult
    {
        public readonly string Output;
        public readonly string Error;
        public readonly int ExitCode;

        public ToolResult(string output, string error, int exitCode)
        {
            Output = output;
            Error = error;
            ExitCode = exitCode;
        }

        public override string ToString()
        {
            return $"{nameof(ExitCode)}={ExitCode}\n{nameof(Error)}={Error}\n{nameof(Output)}={Output}";
        }
    }
}