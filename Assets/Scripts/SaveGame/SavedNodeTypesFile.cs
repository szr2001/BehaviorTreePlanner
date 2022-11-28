using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.SaveGame
{
    [Serializable]
    public class SavedNodeTypesFile
    {
        public NodeType[] Nodetypes;

        public SavedNodeTypesFile(NodeType[] nodetypes)
        {
            Nodetypes = nodetypes;
        }
    }
}
