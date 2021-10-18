using System;

namespace UniTools.CLI
{
    public sealed class AppCenterAttribute : BaseCliToolAttribute
    {
        public AppCenterAttribute() : base(AppCenter.ToolName)
        {
        }

        public override BaseCliTool Create()
        {
            return new AppCenter(
               PathResolver.Default.Execute(AppCenter.ToolName).Output.Split(Environment.NewLine.ToCharArray())?[0],
               CommandLine.Default);
        }
    }
}