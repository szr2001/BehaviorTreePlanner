using System;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SaveLoadManager : MonoBehaviour
    {
        //while the app is loaded in the editor, load the OppenedSavedProject.
        //Save every change back to the same oppenedsavedproject variable
        //then save it to a file. Load ands save the same file but with modified values.
       [field:SerializeField] public EditorManager EditorManager { get; set; }
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
        public void RemoveLayerFromProject(SavedProjectLayer layer)
        {
            if (ActiveProjectLayer == layer)
            {
                return;
            }
            EditorManager.ProjectsManager.OpenedProject.Layers.Remove(layer);
        }
        public async Task SaveProject()
        {
            await Task.CompletedTask;
            EditorManager.ProjectsManager.WriteOpenedProjectFile();
        }
        public async Task SaveToLayer(SavedProjectLayer projectlayer)
        {
            //show loading screen
            projectlayer = await ConvertSceeneToSavedLayer();
            //remove loadingscreen
            await Task.CompletedTask;
        }
        private async Task<SavedProjectLayer> ConvertSceeneToSavedLayer()
        {
            SavedProjectLayer savedProjectLayer = null;
            await Task.CompletedTask;
            return savedProjectLayer;
        }
        public async Task LoadLayer(SavedProjectLayer projectlayer)
        {
            ActiveProjectLayer = projectlayer;
            //ClearScreen();
            //show loading screen
            //await loading and spawning
            //remove loadingscreen
            await Task.CompletedTask;
        }
    }
}
