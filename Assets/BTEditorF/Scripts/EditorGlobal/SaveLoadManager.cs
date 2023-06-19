using BehaviorTreePlanner.Nodes;
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
        [field: SerializeField] public SavedProjectLayer ActiveProjectLayer { get; set; }   
        [field: SerializeField] public GameObject CameraCanvas { get; set; }   
        [field: SerializeField] public GameObject LoadingScreenPrefabReff { get; set; }

        private GameObject loadingScreen;

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
            EditorManager.ProjectsManager.OpenedProject.Layers.Remove(layer);
            _ = SaveProject();
        }
        public void AddLayerToProject(SavedProjectLayer layer)
        {
            EditorManager.ProjectsManager.OpenedProject.Layers.Add(layer);
            _ = SaveProject();
        }
        private async Task SaveProject()
        {
            ShowLoadingScreen();
            await EditorManager.ProjectsManager.SaveOpenedProjectFile();
            HideLoadingScreen();
        }
        public async Task SaveLayer(SavedProjectLayer projectlayerreff)//might not work
        {
            //show loading screen
            projectlayerreff = await ConvertSceeneToSavedLayer();
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
            ShowLoadingScreen();
            ActiveProjectLayer = projectlayer;
            ClearScreen();
            //await loading and spawning
            await Task.CompletedTask;
            HideLoadingScreen();
        }
        public void ShowLoadingScreen()
        {
            if(loadingScreen != null)
            {
                HideLoadingScreen();
            }
            loadingScreen = Instantiate(LoadingScreenPrefabReff, CameraCanvas.transform);
        }
        public void HideLoadingScreen()
        {
            loadingScreen.GetComponent<LoadingScreen>().ClearLoadingSCreen();
        }
    }
}
