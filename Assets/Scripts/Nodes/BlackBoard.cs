using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class BlackBoard : NodeBase
    {
        protected override void Awake()
        {
            NodeD = new NodeDesign(null, null, new Color(0f, 0.87f, 1), Color.white);
            base.Awake();
        }
    }
}