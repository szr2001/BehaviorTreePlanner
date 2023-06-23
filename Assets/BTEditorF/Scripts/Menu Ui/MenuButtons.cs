using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTreePlanner
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] private EditorManager editorManager;

        public void CallSaveProject()
        {
            _ = editorManager.SaveLoadManager.SaveProject();
        }
        public void CallReturnToMenu()
        {
            _ = editorManager.SaveLoadManager.BackToMenu();

        }
    }
}
