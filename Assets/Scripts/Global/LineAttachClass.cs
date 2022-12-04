using BehaviorTreePlanner.Lines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class LineAttachClass 
    {
        public Line Atachedline { get; set; }
        public GameObject parent { get; set; }
        public GameObject attachTrigger { get; set; }
        public LineAttachClass(GameObject parent, GameObject attachTrigger)
        {
            this.parent = parent;
            this.attachTrigger = attachTrigger;
        }
        public void SetLineLocation()
        {
            if (Atachedline != null)
            {
                Atachedline.ChangePoint2(attachTrigger.transform.position);
            }
        }
        public void DeatachLine()
        {
            Atachedline = null;
        }
        public void AttachLine(Line Line)
        {
            if (Atachedline != null)
            {
                Atachedline.DestroyLine();
            }
            Atachedline = Line;
            Atachedline.SetIsMoving(false);
            Atachedline.ChangePoint2(attachTrigger.transform.position);
        }
        public void DeleteLine()
        {
            if (Atachedline != null)
            {
                Atachedline.DestroyLine();
            }
        }
    }
}
