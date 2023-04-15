using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class ProjectNode : MovingNode
    {
        private SavedProject project;
        public override void InitializeNode(NodeDesign nd, EditorManager editormanager)
        {
            base.InitializeNode(nd, editormanager);
            NodeD = new NodeDesign(null, null, new Color(0.57f, 0.3f, 1), Color.white);
        }
        public void OpenProjectNode()
        {
             
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
