using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class LayerNode : MovingNode
    {
        private SavedProjectLayer projectLayer;

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
        public override void InitializeLoad(SavedNodeBase savedata)
        {
            base.InitializeLoad(savedata);
        }

        public override void Load()
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeNode(NodeDesign nd, EditorManager editormanager,SavedProjectLayer projectlayer)
        {
            base.InitializeNode(nd, editormanager);
            projectLayer = projectlayer;
        }
        public void OpenProjectNode()
        {
             
        }
        public override void StartMoveObj()
        {
            NodeHighLight.SetActive(true);
            Debug.Log(projectLayer is null,gameObject);//dell
        }

        public override void StopMoveObj()
        {
            NodeHighLight.SetActive(false);
        }
    }
}
