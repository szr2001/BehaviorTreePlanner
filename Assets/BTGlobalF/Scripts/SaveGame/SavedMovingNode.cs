using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
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
