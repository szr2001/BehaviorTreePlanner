using BehaviorTreePlanner.Nodes;
using System;

namespace BehaviorTreePlanner.Global
{
    [Serializable]
    public class SavedMovingNode : SavedNodeBase
    {
        public float[] Position = new float[2];
        public NodeDesign Nd;

        public SavedMovingNode(float[] position, NodeDesign nd, int nodeIndex, int atachedToLineIndex, int spawnedLineIndex) : base(nodeIndex, atachedToLineIndex, spawnedLineIndex)
        {
            Position = position;
            Nd = nd;
        }
    }
}
