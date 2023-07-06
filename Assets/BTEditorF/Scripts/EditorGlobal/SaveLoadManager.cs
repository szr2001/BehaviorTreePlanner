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
        public static SaveLoadManager Instance;
        [field: SerializeField] public SavedProjectLayer ActiveProjectLayer { get; set; } //why its not saving reff to OpenedProject?  
        [field: SerializeField] public GameObject CameraCanvas { get; set; }
        [field: SerializeField] public GameObject LoadingScreenPrefabReff { get; set; }

        private GameObject loadingScreen;

        public delegate void LayerUpdated(string layername);
        public event LayerUpdated OnLayerUpdated;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            LoadProject();
        }

        public void ClearScreen()
        {
            //nodes automaticaly delete any atached lines
            try
            {
                while (SpawnManager.Instance.ActiveNodes.Count > 0)
                {
                    SpawnManager.Instance.ActiveNodes[0].GetComponent<IObjDestroyable>().DestroyObject();
                }
                if(SpawnManager.Instance.ActiveBlackBoard != null)
                {
                    SpawnManager.Instance.ActiveBlackBoard.GetComponent<IObjDestroyable>().DestroyObject();
                }
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void RemoveLayerFromProject(SavedProjectLayer layer)
        {
            ProjectsManager.Instance.OpenedProject.Layers.Remove(layer);
            _ = SaveProject();
        }

        public void AddLayerToProject(SavedProjectLayer layer)
        {
            ProjectsManager.Instance.OpenedProject.Layers.Add(layer);
            _ = SaveProject();
        }
        private void LoadProject()
        {
            _ = LoadLayer(ProjectsManager.Instance.OpenedProject.Layers[0]);
            loadNodeTypes();
        }
        private void loadNodeTypes()
        {
            EditorUiManager.Instance.NodeMenu.Load(ProjectsManager.Instance.OpenedProject.NodeTypes);
        }
        private void SaveNodeTypes()
        {
            ProjectsManager.Instance.OpenedProject.NodeTypes = EditorUiManager.Instance.NodeMenu.Save();
        }
        public async Task SaveProject()
        {
            ShowLoadingScreen();
            await SaveActiveLayer();
            SaveNodeTypes();
            await ProjectsManager.Instance.SaveOpenedProjectFile();
            HideLoadingScreen();
        }

        public async Task SaveActiveLayer()
        {
            SavedProjectLayer NewLayerData = await ConvertSceeneToSavedLayer();
            ActiveProjectLayer.SavedNodes = NewLayerData.SavedNodes;
            ActiveProjectLayer.SavedLinePoints = NewLayerData.SavedLinePoints;
            ActiveProjectLayer.BlackBoard = NewLayerData.BlackBoard;
        }

        public async Task BackToMenu()
        {
            //save before quit
            await SaveProject();
            //destroy the project manager,settings manager and all their data because it is
            //generated again in the menu
            Destroy(ProjectsManager.Instance.gameObject);
            Destroy(SettingsManager.Instance.gameObject);
            //load menu
            SceneManager.LoadScene("BTMenu");
        }

        private async Task<SavedProjectLayer> ConvertSceeneToSavedLayer()//rework async stuff
        {
            List<SavedNodeBase> NewNodes = new();
            List<SavedLinePoint> NewLines = new();
            SavedNodeBase blackboard = new(-1,-1,-1);

            //asign an unique index based on list order to each line and node for indentifing purposes
            try
            {
                await Task.Run(() => 
                {
                    for (int index = 0; index < SpawnManager.Instance.ActiveNodes.Count; index++)
                    {
                        SpawnManager.Instance.ActiveNodes[index].InitializeSave(index);
                    }
                    for (int index = 0; index < SpawnManager.Instance.ActiveLines.Count; index++)
                    {
                        SpawnManager.Instance.ActiveLines[index].InitializeSave(index);
                    }
                    if(SpawnManager.Instance.ActiveBlackBoard != null)
                    {
                        SpawnManager.Instance.ActiveBlackBoard.InitializeSave(-1);
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            try
            {
                //call save on each node/line CANT RUN ON NEW THREAD
                foreach (NodeBase node in SpawnManager.Instance.ActiveNodes)
                {
                    SavedNodeBase savedn = node.Save();
                    NewNodes.Add(savedn);
                }

                foreach (LinePoint line in SpawnManager.Instance.ActiveLines)
                {
                    SavedLinePoint savedp = line.Save();
                    NewLines.Add(savedp);
                }
                if(SpawnManager.Instance.ActiveBlackBoard != null)
                {
                    blackboard =  SpawnManager.Instance.ActiveBlackBoard.Save();
                }

            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            return new SavedProjectLayer(NewNodes, NewLines, blackboard);
        }

        private async Task ConvertSavedLayerToScene()//try add multithreading
        {
            //initialize load (asign the corect index and set position)
            try
            {
                foreach(SavedNodeBase nodedata in ActiveProjectLayer.SavedNodes)
                {
                    NodeBase spawnedNode;
                    if(nodedata.GetType() == typeof(SavedNode))
                    {
                        spawnedNode = SpawnManager.Instance.SpawnNode(null,false);
                        spawnedNode.InitializeLoad(nodedata);
                    }
                    else if(nodedata.GetType() == typeof(SavedLayerNode))
                    {
                        SavedLayerNode savedn = (SavedLayerNode)nodedata;
                        foreach(SavedProjectLayer layer in ProjectsManager.Instance.OpenedProject.Layers)
                        {
                            if (layer.LayerName == savedn.LayerName)
                            {
                                spawnedNode = SpawnManager.Instance.SpawnLayerNode(layer, false);
                                spawnedNode.InitializeLoad(nodedata);
                            }
                        }
                    }
                }
                NodeBase blackboard = SpawnManager.Instance.SpawnBlackBoardNode();
                blackboard.InitializeLoad(ActiveProjectLayer.BlackBoard);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            try
            {
                foreach (SavedLinePoint linedata in ActiveProjectLayer.SavedLinePoints)
                {
                    LinePoint spawnedLine = SpawnManager.Instance.SpawnLinePoint(null,true,false,false);
                    spawnedLine.InitializeLoad(linedata);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            
            //call load(set up refferences between objects)
            try
            {
                foreach (NodeBase node in SpawnManager.Instance.ActiveNodes)
                {
                    node.Load();
                }
                foreach (LinePoint line in SpawnManager.Instance.ActiveLines)
                {
                    line.Load();
                }
                SpawnManager.Instance.ActiveBlackBoard.Load();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, gameObject);
            }
            await Task.CompletedTask;
        }

        public async Task LoadLayer(SavedProjectLayer projectlayer)
        {
            if(ActiveProjectLayer.LayerName == projectlayer.LayerName)
            {
                return;
            }

            ShowLoadingScreen();
            //Invoke event for buttons to update
            OnLayerUpdated?.Invoke(projectlayer.LayerName);

            //save the curent active layer before loading another layer
            await SaveActiveLayer();

            ActiveProjectLayer = projectlayer;

            //clear the data from the other layer
            ClearScreen();
            
            //await loading of saved data and spawning it
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
            loadingScreen.GetComponent<LoadingScreen>().ClearLoadingScreen();
            loadingScreen = null;
        }
    }
}
