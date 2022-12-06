using BehaviorTreePlanner.Lines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class LineAttachClass 
    {
        public Line Atachedline { get; set; }
        public GameObject Parent { get; set; }
        public GameObject AttachTrigger { get; set; }
        public LineAttachClass(GameObject parent, GameObject attachTrigger)
        {
            this.Parent = parent;
            this.AttachTrigger = attachTrigger;
        }
        public void SetLineLocation()
        {
            if (Atachedline != null)
            {
                Atachedline.ChangePoint2(AttachTrigger.transform.position);
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
            Atachedline.ChangePoint2(AttachTrigger.transform.position);
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
