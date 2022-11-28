using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.SaveGame
{
    [Serializable]
    public class SaveGameFile
    {
        public NodeSaveInfo[] savedNodes;
        public NodeSaveInfo[] savedLines;

        public SaveGameFile(NodeSaveInfo[] savedNodes, NodeSaveInfo[] savedLines)
        {
            this.savedNodes = savedNodes;
            this.savedLines = savedLines;
        }
    }
}
