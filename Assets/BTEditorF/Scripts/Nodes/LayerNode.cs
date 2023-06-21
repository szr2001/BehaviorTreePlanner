using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class LayerNode : MovingNode
    {
        private SavedProjectLayer projectLayer;

        public override SavedNodeBase Save()
        {
            float[] Nodepos = new float[] { gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z };

            return new SavedLayerNode //get rid of layer index and use layer name as an identifier
                (
                    projectLayer.LayerName,
                    Nodepos,
                    NodeD,
                    SaveIndex,
                    lineHandler.AttachedPoint.SaveIndex,
                    lineHandler.SpawnedPoint.SaveIndex
                );
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
