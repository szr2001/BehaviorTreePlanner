using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner.Nodes
{
    public class LayerNode : MovingNode
    {
        private SavedProjectLayer projectLayer;
        [SerializeField] private Text layerNameT;

        public override SavedNodeBase Save()
        {
            float[] Nodepos = new float[] { gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z };

            return new SavedLayerNode
                (
                    projectLayer.LayerName,
                    Nodepos,
                    NodeD,
                    SaveIndex,
                    lineHandler.AttachedPoint != null ? lineHandler.AttachedPoint.SaveIndex : -1,
                    lineHandler.SpawnedPoint != null ? lineHandler.SpawnedPoint.SaveIndex : -1
                );
        }

        int doubleClick = 0;
        bool isdoubleclicking = false;
        public void CallOpenLayer()
        {
            doubleClick++;
            
            if (!isdoubleclicking)
            {
                StartCoroutine(DoubleClickDelay());
                isdoubleclicking = true;
            }

            if(doubleClick >= 2)
            {
                SavedProjectLayer layertoopen = null;
                foreach (SavedProjectLayer layer in ProjectsManager.Instance.OpenedProject.Layers)
                {
                    if (layer.LayerName == projectLayer.LayerName)
                    {
                        layertoopen = layer;
                    }
                }
                if (layertoopen != null)
                {
                    _ = SaveLoadManager.Instance.LoadLayer(layertoopen);
                }
                else
                {
                    DestroyObject();
                }
                doubleClick = 0;
                isdoubleclicking = false;
            }
        }

        private IEnumerator DoubleClickDelay()
        {
            yield return new WaitForSeconds(.2f);
            doubleClick = 0;
            isdoubleclicking = false;
        }

        public override void InitializeLoad(SavedNodeBase savedata)
        {
            base.InitializeLoad(savedata);
            SavedLayerNode savedlayerdata = savedata as SavedLayerNode;
            layerNameT.text = savedlayerdata.LayerName;
        }

        public override void Load()
        {
            base.Load();
        }

        public override void InitializeNode(NodeDesign nd,SavedProjectLayer projectlayer)
        {
            base.InitializeNode(nd);
            projectLayer = projectlayer;
            layerNameT.text = projectLayer.LayerName;
        }

        public override void StartMoveObj()
        {
            NodeHighLight.SetActive(true);
        }

        public override void StopMoveObj()
        {
            NodeHighLight.SetActive(false);
        }
    }
}
