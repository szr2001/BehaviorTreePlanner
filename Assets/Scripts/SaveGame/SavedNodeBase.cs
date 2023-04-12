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
        //atached and spawned lines index
        public int Index;
        public string NodeType;
        public float[] Position = new float[3];
        public NodeDesign Nd;

        //For ProjectNode
        public string ProjectFilePath;
    }
}
