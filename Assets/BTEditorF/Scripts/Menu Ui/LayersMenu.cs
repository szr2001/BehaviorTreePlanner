using BehaviorTreePlanner.MenuUi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace BehaviorTreePlanner
{
    public class LayersMenu : MonoBehaviour
    {
        [SerializeField] private EditorManager editorManager;
        [SerializeField] private GameObject LayerNodeButtonPrefabReff;
        [SerializeField] private GameObject LayersHolder;

        private List<LayerNodeButton> layerButtons = new();// maybe needs to be gameobject instead of layernodebutton
        private LayerNodeButton ActiveLayerNode;

        void Start()
        {
            SpawnLayerButtons();
        }
        private void SpawnLayerButtons()
        {
            foreach (var layer in editorManager.ProjectsManager.OpenedProject.Layers)
            {
                GameObject LayerNodeButton = GameObject.Instantiate(LayerNodeButtonPrefabReff);
                LayerNodeButton layernode =  LayerNodeButton.GetComponent<LayerNodeButton>();
                layernode.InitializeNodeButton(this, layer);
                LayerNodeButton.transform.SetParent(LayersHolder.transform);
                LayerNodeButton.transform.localPosition = new Vector3(LayerNodeButton.transform.position.x, LayerNodeButton.transform.position.y, 0);
                LayerNodeButton.transform.localScale = Vector3.one;
                layerButtons.Add(layernode);
            }

            ActiveLayerNode = layerButtons[0].GetComponent<LayerNodeButton>();
            ActiveLayerNode.HighLight();

            _ = editorManager.SaveLoadManager.LoadLayer(ActiveLayerNode.projectLayer);
        }
    }
}
