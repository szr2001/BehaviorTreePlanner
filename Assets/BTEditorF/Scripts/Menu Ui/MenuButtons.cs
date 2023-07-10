using BehaviorTreePlanner.Global;
using UnityEngine;


namespace BehaviorTreePlanner.MenuUi
{
    public class MenuButtons : MonoBehaviour
    {

        public void CallSaveProject()
        {
            _ = SaveLoadManager.Instance.SaveProject();
        }
        public void CallReturnToMenu()
        {
            _ = SaveLoadManager.Instance.BackToMenu();

        }
        public void CallShowSettings()
        {
            SettingsManager.Instance.ShowSettingsScreen();
        }
    }
}
