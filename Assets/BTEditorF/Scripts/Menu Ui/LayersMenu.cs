using BehaviorTreePlanner.MenuUi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace BehaviorTreePlanner
{
    public class LayersMenu : MonoBehaviour
    {
        [SerializeField] private EditorManager editorManager;
        [SerializeField] private GameObject LayerNodeButtonPrefabReff;
        [SerializeField] private GameObject LayersHolder;

        private List<LayerNodeButton> layerButtons = new();// maybe needs to be gameobject instead of layernodebutton
        private LayerNodeButton ActiveLayerButton;

        void Start()
        {
            SpawnLayerButtons();
        }
        private void SpawnLayerButtons()
        {
            foreach (var layer in editorManager.ProjectsManager.OpenedProject.Layers)
            {
                CreatelayerNodeButton(layer);
            }

            ActiveLayerButton = layerButtons[0].GetComponent<LayerNodeButton>();
            ActiveLayerButton.HighLight();

            _ = editorManager.SaveLoadManager.LoadLayer(ActiveLayerButton.projectLayer);
        }
        private GameObject CreatelayerNodeButton(SavedProjectLayer layer)
        {
            GameObject LayerNodeButton = GameObject.Instantiate(LayerNodeButtonPrefabReff);
            LayerNodeButton layernode = LayerNodeButton.GetComponent<LayerNodeButton>();
            layernode.InitializeNodeButton(this, layer);
            LayerNodeButton.transform.SetParent(LayersHolder.transform);
            LayerNodeButton.transform.localPosition = new Vector3(LayerNodeButton.transform.position.x, LayerNodeButton.transform.position.y, 0);
            LayerNodeButton.transform.localScale = Vector3.one;
            layerButtons.Add(LayerNodeButton.GetComponent<LayerNodeButton>());
            return LayerNodeButton;
        }
        public void DeleteLayer(LayerNodeButton layerButton)
        {
            if (editorManager.SaveLoadManager.ActiveProjectLayer == layerButton.projectLayer)
            {
                return;
            }
            editorManager.SaveLoadManager.RemoveLayerFromProject(layerButton.projectLayer);
            layerButtons.Remove(layerButton);
            Destroy(layerButton.gameObject);
        }
        public void CreateLayer()
        {
            SavedProjectLayer NewLayer = new(new List<SavedNodeBase>(),new List<SavedLinePoint>());
            CreatelayerNodeButton(NewLayer);
            editorManager.ProjectsManager.OpenedProject.Layers.Add(NewLayer);
            editorManager.ProjectsManager.WriteOpenedProjectFile();
        }
        public void SpawnLayerNode(SavedProjectLayer layer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                editorManager.MoveObjectsManager.ClearMovableObj();
                editorManager.SpawnManager.SpawnLayerNode(layer);
            }
        }
        public void OpenLayer(LayerNodeButton layerButton)
        {
            ActiveLayerButton.UnHighlight();
            layerButton.HighLight();
            ActiveLayerButton = layerButton;
            _ = editorManager.SaveLoadManager.LoadLayer(layerButton.projectLayer);
        }
    }
}
