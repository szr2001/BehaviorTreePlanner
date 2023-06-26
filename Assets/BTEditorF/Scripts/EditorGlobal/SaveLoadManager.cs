using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BehaviorTreePlanner
{
    public class SaveLoadManager : MonoBehaviour
    {
        //while the app is loaded in the editor, load the OppenedSavedProject.
        //Save every change back to the same oppenedsavedproject variable
        //then save it to a file. Load ands save the same file but with modified values.
        [field: SerializeField] public EditorManager EditorManager { get; set; }
        [field: SerializeField] public SavedProjectLayer ActiveProjectLayer { get; set; } //why its not saving reff to OpenedProject?  
        [field: SerializeField] public GameObject CameraCanvas { get; set; }
        [field: SerializeField] public GameObject LoadingScreenPrefabReff { get; set; }

        private GameObject loadingScreen;

        public void ClearScreen()
        {
            //nodes automaticaly delete any atached lines
            try
            {
                while (EditorManager.SpawnManager.ActiveNodes.Count > 0)
                {
                    EditorManager.SpawnManager.ActiveNodes[0].GetComponent<IObjDestroyable>().DestroyObject();
                }
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
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
        public async Task SaveProject()
        {
            ShowLoadingScreen();
            await SaveActiveLayer();
            await EditorManager.ProjectsManager.SaveOpenedProjectFile();
            HideLoadingScreen();
        }
        public async Task SaveActiveLayer()
        {
            SavedProjectLayer NewLayerData = await ConvertSceeneToSavedLayer();
            ActiveProjectLayer.SavedNodes = NewLayerData.SavedNodes;
            ActiveProjectLayer.SavedLinePoints = NewLayerData.SavedLinePoints;
        }
        public async Task BackToMenu()
        {
            await SaveProject();
            SceneManager.LoadScene("BTMenu");
        }
        private async Task<SavedProjectLayer> ConvertSceeneToSavedLayer()
        {
            List<SavedNodeBase> NewNodes = new();
            List<SavedLinePoint> NewLines = new();

            await Task.Run(() =>
            {
                //asign an unique index based on list order to each line and node for indentifing purposes
                try
                {
                    for (int index = 0; index < EditorManager.SpawnManager.ActiveNodes.Count; index++)
                    {
                        EditorManager.SpawnManager.ActiveNodes[index].InitializeSave(index);
                    }
                    for (int index = 0; index < EditorManager.SpawnManager.ActiveLines.Count; index++)
                    {
                        EditorManager.SpawnManager.ActiveLines[index].InitializeSave(index);
                    }
                }
                catch(Exception ex)
                {
                    Debug.LogException(ex,gameObject);
                }
            });
            try
            {
                //call save on each node/line CANT RUN ON NEW THREAD
                foreach (NodeBase node in EditorManager.SpawnManager.ActiveNodes)
                {
                    SavedNodeBase savedn = node.Save();
                    NewNodes.Add(savedn);
                }

                foreach (LinePoint line in EditorManager.SpawnManager.ActiveLines)
                {
                    SavedLinePoint savedp = line.Save();
                    NewLines.Add(savedp);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            return new SavedProjectLayer(NewNodes, NewLines);
        }
        private async Task ConvertSavedLayerToScene()
        {
            try
            {
                //initialize load (asign the corect index and set position)
                foreach(SavedNodeBase nodedata in ActiveProjectLayer.SavedNodes)
                {
                    NodeBase spawnedNode;
                    if(nodedata.GetType() == typeof(SavedNode))
                    {
                        spawnedNode = EditorManager.SpawnManager.SpawnNode(null,false);
                        spawnedNode.InitializeLoad(nodedata,EditorManager);
                    }
                    else if(nodedata.GetType() == typeof(SavedLayerNode))
                    {
                        spawnedNode = EditorManager.SpawnManager.SpawnLayerNode(null,false);
                        spawnedNode.InitializeLoad(nodedata,EditorManager);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            try
            {
                foreach (SavedLinePoint linedata in ActiveProjectLayer.SavedLinePoints)
                {
                    LinePoint spawnedLine = EditorManager.SpawnManager.SpawnLinePoint(null,true,false,false);
                    spawnedLine.InitializeLoad(linedata);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            try
            {
                //call load(set up refferences between objects)
                foreach (NodeBase node in EditorManager.SpawnManager.ActiveNodes)
                {
                    node.Load();
                }
                foreach (LinePoint line in EditorManager.SpawnManager.ActiveLines)
                {
                    line.Load();
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            await Task.CompletedTask;
        }
        public async Task LoadLayer(SavedProjectLayer projectlayer)
        {
            ShowLoadingScreen();
            //save the curent active layer before loading another layer
            await SaveActiveLayer();
            //clear the data from the other layer
            ClearScreen();
            //await loading of data and spawning stuff
            ActiveProjectLayer = projectlayer;

            await ConvertSavedLayerToScene();
            
            HideLoadingScreen();
}
        public void ShowLoadingScreen()
        {
            if(loadingScreen != null)
            {
                return;
            }
            loadingScreen = Instantiate(LoadingScreenPrefabReff, CameraCanvas.transform);
        }
        public void HideLoadingScreen()
        {
            if(loadingScreen == null ) 
            {
                return;
            }
            loadingScreen.GetComponent<LoadingScreen>().ClearLoadingSCreen();
            loadingScreen = null;
        }
    }
}
