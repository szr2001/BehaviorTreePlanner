using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedProjectLayer 
    {
        public List<SavedNodeBase> SavedNodes;
        public List<SavedLinePoint> SavedLinePoints;
        public SavedProjectLayer(List<SavedNodeBase> savedNodes, List<SavedLinePoint> savedLinePoints)
        {
            SavedNodes = savedNodes;
            SavedLinePoints = savedLinePoints; 
        }


    }
}
