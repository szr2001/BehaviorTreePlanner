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
        [field: SerializeField] public EditorManager editorManager { get; set; }
        [SerializeField] private GameObject LayerNodeButtonPrefabReff;
        [SerializeField] private GameObject LayersHolder;

        private List<LayerNodeButton> layerButtons = new();

        void Awake()
        {
            SpawnLayerButtons();
        }

        private void SpawnLayerButtons()
        {
            foreach (SavedProjectLayer layer in editorManager.ProjectsManager.OpenedProject.Layers)
            {
                CreatelayerNodeButton(layer);
            }
            layerButtons[0].HighLight();
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
            if (layerButtons[0] == layerButton)
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
            editorManager.SaveLoadManager.AddLayerToProject(NewLayer);
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
            _ = editorManager.SaveLoadManager.LoadLayer(layerButton.projectLayer);
        }
    }
}
