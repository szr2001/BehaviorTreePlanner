using BehaviorTreePlanner.Nodes;
using System;

namespace BehaviorTreePlanner.Global
{
    [Serializable]
    public class SavedLayerNode : SavedMovingNode
    {
        public string LayerName;

        public SavedLayerNode(string layername, float[] position, NodeDesign nd, int nodeIndex, int atachedToLineIndex, int spawnedLineIndex) : base(position, nd, nodeIndex, atachedToLineIndex, spawnedLineIndex)
        {
            LayerName = layername;
        }
    }
}
