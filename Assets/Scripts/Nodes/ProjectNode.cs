using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class ProjectNode : MovingNode
    {
        protected override void Awake()
        {
            NodeD = new NodeDesign(null, null, new Color(0.57f, 0.3f, 1), Color.white);
            base.Awake();
        }
    }
}
