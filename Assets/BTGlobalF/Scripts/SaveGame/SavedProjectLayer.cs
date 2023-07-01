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
        public SavedNodeBase BlackBoard;
        public List<SavedNodeBase> SavedNodes;
        public List<SavedLinePoint> SavedLinePoints;
        public string LayerName;
        public SavedProjectLayer(List<SavedNodeBase> savedNodes, List<SavedLinePoint> savedLinePoints, SavedNodeBase blackBoard)
        {
            SavedNodes = savedNodes;
            SavedLinePoints = savedLinePoints;
            BlackBoard = blackBoard;
        }


    }
}
