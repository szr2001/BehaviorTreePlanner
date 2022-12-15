using System;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    [Serializable]
    public class NodeDesign
    {
        public string type;
        public string name;
        public Color PrimaryCollor;
        public Color SecondaryCollor;

        public NodeDesign(string type, string name, Color primaryCollor, Color secondaryCollor)
        {
            this.type = type;
            this.name = name;
            this.PrimaryCollor = primaryCollor;
            this.SecondaryCollor = secondaryCollor;
        }
    }
}