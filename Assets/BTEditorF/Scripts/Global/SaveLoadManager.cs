using BehaviorTreePlanner.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SaveLoadManager : MonoBehaviour
    {

        //test clear
        public void ClearScreen()
        {
            for (int i = 0; i < SavedReff.ActiveNodes.Count; i++)
            {
                try
                {
                    SavedReff.ActiveNodes[i].GetComponent<IObjDestroyable>().DestroyObject();
                }
                catch { Debug.Log("err"); }
            }
            for (int i = 0; i < SavedReff.ActiveLines.Count; i++)
            {
                try
                {
                    SavedReff.ActiveLines[i].GetComponent<IObjDestroyable>().DestroyObject();
                }
                catch { Debug.Log("err"); }
            }
        }
    }
}
