using BehaviorTreePlanner.Lines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class LineAttachClass 
    {
        public GameObject Parent { get; set; }
        public GameObject AttachTrigger { get; set; }
        public LineAttachClass(GameObject parent, GameObject attachTrigger)
        {
            this.Parent = parent;
            this.AttachTrigger = attachTrigger;
        }

    }
}
