using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedStaticNode : SavedNodeBase
    {
        public SavedStaticNode(int nodeIndex, int atachedpointIndex, int spawnedpointIndex) : base(nodeIndex, atachedpointIndex, spawnedpointIndex)
        {
        }
    }
}
