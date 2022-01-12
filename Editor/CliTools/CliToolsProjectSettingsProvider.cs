using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniTools.CLI.Editor
{
    public sealed class CliToolsProjectSettingsProvider : SettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider Register() =>
            new CliToolsProjectSettingsProvider($"Project/{nameof(UniTools)}/CLI");

        private readonly List<CliToolEditorPresenter> m_presenters = new List<CliToolEditorPresenter>();

        private CliToolsProjectSettingsProvider(string path)
            : base(path, SettingsScope.Project)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            Show();
        }

        public override void OnGUI(string searchContext)
        {
            foreach (CliToolEditorPresenter presenter in m_presenters)
            {
                presenter.Draw();
                EditorGUILayout.Space(1);
            }

            bool refresh = GUILayout.Button("Refresh");
            if (refresh)
            {
                Cli.Refresh();
                m_presenters.Clear();
                Show();
            }

            EditorGUILayout.HelpBox("If the CLI tool is installed but not visible in editor after \"Refresh\", try to: \n-recompile the code base or restart Unity Editor\n-check the PATH in the \"Tools/CLI/UnityEnvironment\".", MessageType.Info);

            if (EditorGUILayout.LinkButton("How to change PATH in UnityEnvironment?"))
            {
                Application.OpenURL("https://github.com/UniToolsTeam/unitools-cli#unitools-cli");
            }
        }

        public override void OnDeactivate()
        {
            m_presenters.Clear();
            base.OnDeactivate();
        }

        private void Show()
        {
            IEnumerable<BaseCliTool> tools = Cli.All;
            m_presenters.Clear();
            foreach (BaseCliTool tool in tools)
            {
                m_presenters.Add(new CliToolEditorPresenter(tool));
            }
        }
    }
}