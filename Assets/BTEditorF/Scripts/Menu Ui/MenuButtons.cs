using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTreePlanner
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
