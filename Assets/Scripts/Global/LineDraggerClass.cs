using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class LineDraggerClass
    {
        public GameObject Parent { get; set; }
        public GameObject LineTrigger { get; set; }

        public LineDraggerClass(GameObject parent, GameObject lineTrigger)
        {
            this.Parent = parent;
            this.LineTrigger = lineTrigger;
        }
    }
}
