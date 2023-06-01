using BehaviorTreePlanner.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SaveLoadManager : MonoBehaviour
    {

        public EditorManager EditorManager;
        //test clear
        public void ClearScreen()
        {
            for (int i = 0; i < EditorManager.SpawnManager.ActiveNodes.Count; i++)
            {
                try
                {
                    EditorManager.SpawnManager.ActiveNodes[i].GetComponent<IObjDestroyable>().DestroyObject();
                }
                catch { Debug.Log("err"); }
            }
            for (int i = 0; i < EditorManager.SpawnManager.ActiveLines.Count; i++)
            {
                try
                {
                    EditorManager.SpawnManager.ActiveLines[i].GetComponent<IObjDestroyable>().DestroyObject();
                }
                catch { Debug.Log("err"); }
            }
        }

        public void UpdateProjectFile()
        {
            //convert scene to savedproject
            //update the project file and replace the ProjectManager.OpenedProject using ProjectManager.EdidProjectFile
        }

        public void ProcessProjectFile()
        {
            //load file in ProjectManager.OpenedProject
        }

        private SavedProject ConvertSceneToSavedProject()
        {
            SavedProject SceneProject = null;

            return SceneProject;
        }
    }
}
