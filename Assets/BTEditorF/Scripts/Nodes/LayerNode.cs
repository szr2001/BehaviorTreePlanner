using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class LayerNode : MovingNode
    {
        private SavedProjectLayer projectLayer;

        public override NodeBase CreateNode()
        {
            return new LayerNode();
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
