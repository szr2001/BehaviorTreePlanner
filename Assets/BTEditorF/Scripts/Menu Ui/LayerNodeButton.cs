using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class LayerNodeButton : MonoBehaviour
    {
        private LayersMenu layersMenu;
        public SavedProjectLayer projectLayer { get; set; }
        [SerializeField] private Text nodeName;
        [SerializeField] private Text Layer;
        [SerializeField] private Image NodeColor;
        
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
            layersMenu.DeleteLayer(this);
        }
        int clickcount = 0;
        public void CallOpenLayer()
        {
            clickcount++;
            _ = CallOpenLayerDoubleClickTrigger();
        }
        private async Task CallOpenLayerDoubleClickTrigger()
        {
            if (clickcount == 2)
            {
                layersMenu.OpenLayer(this);
                clickcount = 0;
                return;
            }
            await Task.Delay(500);
            clickcount = 0;
        }
        public void CallSpawnlayerNode()
        {
            layersMenu.SpawnLayerNode(projectLayer);
        }
    }
}
