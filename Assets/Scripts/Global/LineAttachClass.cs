using BehaviorTreePlanner.Lines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class LineAttachClass 
    {
        public GameObject Atachedline { get; set; }
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
                Atachedline.GetComponent<Line>().ChangePoint2(attachTrigger.transform.position);
            }
        }
        public void DeatachLine()
        {
            Atachedline = null;
        }
        public void AttachLine(GameObject Line)
        {
            if (Atachedline == null)
            {
                Atachedline = Line;
                Atachedline.GetComponent<Line>().SetIsMoving(false);
                Atachedline.GetComponent<Line>().ChangePoint2(attachTrigger.transform.position);
            }
        }
        public void DeleteLine()
        {
            if (Atachedline != null)
            {
                Atachedline.GetComponent<Line>().DestroyLine();
            }
        }
    }
}
