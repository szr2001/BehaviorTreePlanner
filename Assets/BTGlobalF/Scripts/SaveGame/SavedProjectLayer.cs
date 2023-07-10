using System;
using System.Collections.Generic;

namespace BehaviorTreePlanner.Global
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
