using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SaveLoadManager : MonoBehaviour
    {

        public EditorManager EditorManager { get; set; }
        public SavedProjectLayer ActiveProjectLayer { get; set; }   

        public void ClearScreen()//edit to use selected layer
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
            //no need to clear the lists after because DestroyObject removes itself from lists.
        }

        public async Task SaveLayer(SavedProjectLayer projectlayer)
        {
            await Task.CompletedTask;
        }
        public async Task LoadLayer(SavedProjectLayer projectlayer)
        {
            ActiveProjectLayer = projectlayer;
            ClearScreen();
            //show loading screen
            //await loading and spawning
            await Task.CompletedTask;
            //remove loadingscreen
        }

    }
}
