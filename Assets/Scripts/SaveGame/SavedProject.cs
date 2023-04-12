using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedProject 
    {
        //use array index from savedref point/node list for savedline/savednode index
        public SavedProject(List<SavedNodeBase> savedNodes, List<SavedLinePoint> savedLinePoints, int appVersion)
        {
            SavedNodes = savedNodes;
            SavedLinePoints = savedLinePoints; 
            AppVersion = appVersion;
        }

        public List<SavedNodeBase> SavedNodes;
        public List<SavedLinePoint> SavedLinePoints;
        public int AppVersion;

    }
}
