using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedNodeBase
    {
        public int NodeIndex;
        public int AtachedPointIndex;
        public int SpawnedPointIndex;

        public SavedNodeBase(int nodeIndex, int atachedpointIndex, int spawnedpointIndex)
        {
            NodeIndex = nodeIndex;
            AtachedPointIndex = atachedpointIndex;
            SpawnedPointIndex = spawnedpointIndex;
        }
    }
}
