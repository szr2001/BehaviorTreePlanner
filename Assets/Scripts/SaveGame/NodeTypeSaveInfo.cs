using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.SaveGame
{
    [Serializable]
    public class NodeTypeSaveInfo
    {
        public NodeDesign[] Nodetypes;

        public NodeTypeSaveInfo(NodeDesign[] nodetypes)
        {
            Nodetypes = nodetypes;
        }
    }
}
