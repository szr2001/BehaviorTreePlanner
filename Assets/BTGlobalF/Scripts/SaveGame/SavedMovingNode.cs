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
        public float[] Position = new float[3];
        public NodeDesign Nd;
    }
}
