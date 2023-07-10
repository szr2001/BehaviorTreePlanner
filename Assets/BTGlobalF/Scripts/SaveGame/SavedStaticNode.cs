using System;

namespace BehaviorTreePlanner.Global
{
    [Serializable]
    public class SavedStaticNode : SavedNodeBase
    {
        public SavedStaticNode(int nodeIndex, int atachedpointIndex, int spawnedpointIndex) : base(nodeIndex, atachedpointIndex, spawnedpointIndex)
        {
        }
    }
}
