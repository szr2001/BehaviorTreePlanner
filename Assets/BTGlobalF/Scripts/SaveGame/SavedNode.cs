using BehaviorTreePlanner.Nodes;
using System;

namespace BehaviorTreePlanner.Global
{
    [Serializable]
    public class SavedNode : SavedMovingNode
    {
        public SavedNode(float[] position, NodeDesign nd, int nodeIndex, int atachedToLineIndex, int spawnedLineIndex) : base(position, nd, nodeIndex, atachedToLineIndex, spawnedLineIndex)
        {
        }
    }
}
