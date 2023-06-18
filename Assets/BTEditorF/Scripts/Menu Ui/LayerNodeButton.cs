using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class LayerNodeButton : MonoBehaviour
    {
        private LayersMenu layersMenu;
        public SavedProjectLayer projectLayer { get; set; }
        [SerializeField] private GameObject nodeName;
        [SerializeField] private GameObject nodeLayerNr;
        [SerializeField] private Image NodeColor;
        
        //save the layer inside the button dont use the index, because you want to also save thel ayer
        public void InitializeNodeButton(LayersMenu layersmenu, SavedProjectLayer projectlayer)
        {
            layersMenu = layersmenu;
            projectLayer = projectlayer;
        }
        public void HighLight()
        {
            NodeColor.color = new Color(0.6981132f, 0.3984514f, 0.6386044f);
        }
        public void UnHighlight()
        {
            NodeColor.color = new Color(0.5751644f, 0.3066038f, 1);
        }

        public void CallDeleteLayerButton()
        {
            layersMenu.DeleteLayerButton(this);
        }
        public void CallOpenLayer()
        {
            layersMenu.OpenLayer(this);
        }
        public void CallSpawnlayerNode()
        {
            layersMenu.SpawnLayerNode(projectLayer);
        }
    }
}
