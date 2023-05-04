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
        //use List index of nodes/lines from savedref for savedline/savednode index
        //atached and spawned lines index
        // use factory pattern and TypeOf to get type of node ,make new childs for moving node
        public int Index;
        public float[] Position = new float[3];
        public NodeDesign Nd;

        //For ProjectNode
        public string ProjectFilePath;
    }
}
