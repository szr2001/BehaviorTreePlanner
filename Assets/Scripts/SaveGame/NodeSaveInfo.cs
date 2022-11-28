using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.SaveGame
{
    [Serializable]
    public class NodeSaveInfo
    {
        public NodeType nodeType;
        public int[] nodeLocation = new int[2];
        public int[] spawnedLinesIndexes;
        public int atachedLineIndex;

        public NodeSaveInfo(int[] nodeLocation, int[] spawnedLinesIndexes, int atachedLineIndex, NodeType nodeType)
        {
            this.nodeLocation = nodeLocation;
            this.spawnedLinesIndexes = spawnedLinesIndexes;
            this.atachedLineIndex = atachedLineIndex;
            this.nodeType = nodeType;
        }
    }
}
