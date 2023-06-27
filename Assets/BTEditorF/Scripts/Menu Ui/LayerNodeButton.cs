using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class LayerNodeButton : MonoBehaviour
    {
        private LayersMenu layersMenu;
        public SavedProjectLayer projectLayer { get; set; }
        [SerializeField] private InputField layerNodeInput;
        [SerializeField] private Image nodeColor;
        [SerializeField] private Button Ok;
        [SerializeField] private Button Delete;
        [SerializeField] private Button Open;
        [SerializeField] private Text NodeCount;
        [SerializeField] private Text LineCount;
        private bool isEditing = false;
        
        public void InitializeNodeButton(LayersMenu layersmenu, SavedProjectLayer projectlayer)
        {
            layersMenu = layersmenu;
            projectLayer = projectlayer;
            NodeCount.text = projectlayer.SavedNodes.Count.ToString();
            LineCount.text = projectlayer.SavedLinePoints.Count.ToString();
            layerNodeInput.text = projectlayer.LayerName;

        }
        private void Start()
        {
            layersMenu.editorManager.SaveLoadManager.OnLayerUpdated += CheckHighlight;
        }
        ~LayerNodeButton()
        {
            layersMenu.editorManager.SaveLoadManager.OnLayerUpdated -= CheckHighlight;
        }
        private void CheckHighlight(string layername)
        {
            Debug.Log("LAYER CHANGED");

            if(layername == projectLayer.LayerName)
            {
                HighLight();
            }
            else
            {
                UnHighlight();
            }
        }
        public void HighLight()
        {
            nodeColor.color = new Color(0.6981132f, 0.3984514f, 0.6386044f);
        }
        public void UnHighlight()
        {
            nodeColor.color = new Color(0.3466221f, 0.1603774f, 0.6415094f);
        }
        public void ConfirmEdit()
        {
            projectLayer.LayerName = layerNodeInput.text;
            ToggleEdit();
        }
        public void ToggleEdit()
        {
            if (isEditing)
            {
                isEditing = false;
                Open.gameObject.SetActive(true);
                Ok.gameObject.SetActive(false);
                Delete.gameObject.SetActive(false);
                layerNodeInput.interactable = false;
            }
            else
            {
                isEditing = true;
                Open.gameObject.SetActive(false);
                Ok.gameObject.SetActive(true);
                Delete.gameObject.SetActive(true);
                layerNodeInput.interactable = true;
            }
        }
        public void CallDeleteLayerButton()
        {
            layersMenu.DeleteLayer(this);
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
