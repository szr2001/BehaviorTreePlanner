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
        private BTLogger mLogger;
        [field: SerializeField] public SavedProjectLayer ActiveProjectLayer { get; set; }
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
            mLogger = new(this.name, false);
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
                mLogger.Log("ClearScreen", $"cleared screen,deleted {SpawnManager.Instance.ActiveNodes.Count} nodes, {SpawnManager.Instance.ActiveLines.Count} lines");
               
                while (SpawnManager.Instance.ActiveNodes.Count > 0)
                {
                    SpawnManager.Instance.ActiveNodes[0].GetComponent<IObjDestroyable>().DestroyObject();
                }
                if(SpawnManager.Instance.ActiveBlackBoard != null)
                {
                    SpawnManager.Instance.ActiveBlackBoard.GetComponent<IObjDestroyable>().DestroyObject();
                }

            }
            catch (Exception ex)
            {
                mLogger.Log("ClearScreen", $"EXCEPTION TRHOWN:  {ex.Message}");
                Debug.LogException(ex);
            }
        }

        public void RemoveLayerFromProject(SavedProjectLayer layer)
        {
            mLogger.Log("RemoveLayerFromProject", $"Deleted '{layer.LayerName}' layer");
            ProjectsManager.Instance.OpenedProject.Layers.Remove(layer);
            _ = SaveProject();
        }

        public void AddLayerToProject(SavedProjectLayer layer)
        {
            mLogger.Log("AddLayerToProject", $"Added '{layer.LayerName}' layer");
            ProjectsManager.Instance.OpenedProject.Layers.Add(layer);
            _ = SaveProject();
        }
        private void LoadProject()
        {
            mLogger.Log("LoadProject", $"Loading Project");
            _ = LoadLayer(ProjectsManager.Instance.OpenedProject.Layers[0]);
            loadNodeTypes();
        }
        private void loadNodeTypes()
        {
            mLogger.Log("loadNodeTypes", $"loading node types");
            EditorUiManager.Instance.NodeMenu.Load(ProjectsManager.Instance.OpenedProject.NodeTypes);
        }
        private void SaveNodeTypes()
        {
            mLogger.Log("SaveNodeTypes", $"Saving node types");
            ProjectsManager.Instance.OpenedProject.NodeTypes = EditorUiManager.Instance.NodeMenu.Save();
        }
        public async Task SaveProject()
        {
            mLogger.Log("SaveProject", $"Saving project");
            ShowLoadingScreen();
            await SaveActiveLayer();
            SaveNodeTypes();
            await ProjectsManager.Instance.SaveOpenedProjectFile();
            HideLoadingScreen();
        }

        public async Task SaveActiveLayer()
        {
            mLogger.Log("SaveActiveLayer", $"Saving active layer");
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
            Destroy(SoundManager.Instance.gameObject);
            //load menu
            SceneManager.LoadScene("BTMenu");
        }

        private async Task<SavedProjectLayer> ConvertSceeneToSavedLayer()//rework async stuff
        {
            List<SavedNodeBase> NewNodes = new();
            List<SavedLinePoint> NewLines = new();
            SavedNodeBase blackboard = new(-1,-1,-1);

            mLogger.Log("ConvertSceeneToSavedLayer", $"Start convert scene to saved layer");
            //asign an unique index based on list order to each line and node for indentifing purposes
            try
            {
                mLogger.Log("ConvertSceeneToSavedLayer", $"Calling initialize save on objects");
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
                mLogger.Log("ConvertSceeneToSavedLayer", $" Initialize Save EXCEPTION TRHOWN:  {ex.Message}");
                Debug.LogException(ex, gameObject);
            }
            try
            {
                mLogger.Log("ConvertSceeneToSavedLayer", $"Calling Save on objects");
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
                mLogger.Log("ConvertSceeneToSavedLayer", $" Save EXCEPTION TRHOWN:  {ex.Message}");
                Debug.LogException(ex, gameObject);
            }
            return new SavedProjectLayer(NewNodes, NewLines, blackboard);
        }

        private async Task ConvertSavedLayerToScene()//try add multithreading
        {
            //initialize load (asign the corect index and set position)
            mLogger.Log("ConvertSavedLayerToScene", $"Start convert Savedlayer to Scene");
            try
            {
                mLogger.Log("ConvertSavedLayerToScene", $"Spawn Nodes and initialize Load");
                foreach(SavedNodeBase nodedata in ActiveProjectLayer.SavedNodes)
                {
                    NodeBase spawnedNode = null;
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
                        if(spawnedNode == null)
                        {
                            SavedLayerNode castednode = (SavedLayerNode)nodedata;
                            SavedProjectLayer layer2 = new(new List<SavedNodeBase>(),new List<SavedLinePoint>(),new SavedNodeBase(-1,-1,-1));
                            layer2.LayerName = castednode.LayerName;

                            spawnedNode = SpawnManager.Instance.SpawnLayerNode(layer2, false);
                            spawnedNode.InitializeLoad(nodedata);
                            NodeDesign ErrorNodeD = new(null, null, new Color(0.79f, 0.07f, 0.07f), Color.white);
                            spawnedNode.InitializeNode(ErrorNodeD, layer2);
                        }
                    }
                }
                NodeBase blackboard = SpawnManager.Instance.SpawnBlackBoardNode();
                blackboard.InitializeLoad(ActiveProjectLayer.BlackBoard);
            }
            catch (Exception ex)
            {
                mLogger.Log("ConvertSavedLayerToScene", $"Intialize load Nodes EXCEPTION TRHOWN:  {ex.Message}");
                Debug.LogException(ex, gameObject);
            }

            try
            {
                mLogger.Log("ConvertSavedLayerToScene", $"Spawn Lines and initialize Load");
                foreach (SavedLinePoint linedata in ActiveProjectLayer.SavedLinePoints)
                {
                    LinePoint spawnedLine = SpawnManager.Instance.SpawnLinePoint(null,true,false,false);
                    spawnedLine.InitializeLoad(linedata);
                }
            }
            catch (Exception ex)
            {
                mLogger.Log("ConvertSavedLayerToScene", $" Initialize Load Lines EXCEPTION TRHOWN:  {ex.Message}");
                Debug.LogException(ex, gameObject);
            }
            
            //call load(set up refferences between objects)
            try
            {
                mLogger.Log("ConvertSavedLayerToScene", $"Calling LOAD on lines and nodes");
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
                mLogger.Log("ConvertSavedLayerToScene", $"Load EXCEPTION TRHOWN:  {ex.Message}");
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

            mLogger.Log("LoadLayer", $"Loading Layer '{projectlayer.LayerName}'");
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
            mLogger.Log("ShowLoadingScreen", $"Showing Loading Screen");
            loadingScreen = Instantiate(LoadingScreenPrefabReff, CameraCanvas.transform);
        }

        public void HideLoadingScreen()
        {
            if(loadingScreen == null ) 
            {
                return;
            }
            mLogger.Log("HideLoadingScreen", $"Hiding Loading Screen");
            loadingScreen.GetComponent<LoadingScreen>().ClearLoadingScreen();
            loadingScreen = null;
        }
    }
}
