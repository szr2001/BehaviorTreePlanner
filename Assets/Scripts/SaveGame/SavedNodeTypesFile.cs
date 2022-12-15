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
        public NodeDesign[] Nodetypes;

        public SavedNodeTypesFile(NodeDesign[] nodetypes)
        {
            Nodetypes = nodetypes;
        }
    }
}
