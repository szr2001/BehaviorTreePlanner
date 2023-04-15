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
            for (int i = 0; i < EditorManager.ActiveNodes.Count; i++)
            {
                try
                {
                    EditorManager.ActiveNodes[i].GetComponent<IObjDestroyable>().DestroyObject();
                }
                catch { Debug.Log("err"); }
            }
            for (int i = 0; i < EditorManager.ActiveLines.Count; i++)
            {
                try
                {
                    EditorManager.ActiveLines[i].GetComponent<IObjDestroyable>().DestroyObject();
                }
                catch { Debug.Log("err"); }
            }
        }
    }
}
