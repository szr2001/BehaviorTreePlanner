using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedLayerNode : SavedMovingNode
    {
        public string LayerName;

        public SavedLayerNode(string layername,float[] position, NodeDesign nd, int nodeIndex, int atachedToLineIndex, int spawnedLineIndex) : base(position, nd, nodeIndex, atachedToLineIndex, spawnedLineIndex)
        {
            LayerName = layername;
        }
    }
}
