using System;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    [Serializable]
    public class NodeType
    {
        public string type;
        public string name;
        public Color topColor;
        public Color botColor;

        public NodeType(string type, string name, Color topColor, Color botColor)
        {
            this.type = type;
            this.name = name;
            this.topColor = topColor;
            this.botColor = botColor;
        }
    }
}