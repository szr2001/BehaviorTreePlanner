using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class BlackBoard : NodeBase
    {
        public override NodeBase CreateNode()
        {
            return new BlackBoard();
        }
    }
}