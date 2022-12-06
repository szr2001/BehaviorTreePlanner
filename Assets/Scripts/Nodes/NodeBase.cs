using BehaviorTreePlanner.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTreePlanner.Interfaces;
using BehaviorTreePlanner.Lines;

namespace BehaviorTreePlanner
{
    public class NodeBase : MonoBehaviour,IAttachLine
    {
        public LineDraggerClass LineDraggerC { get; set; }
        public LineAttachClass LineAttacherC { get; set; }
        [SerializeField] private GameObject attachTrigger;
        [SerializeField] private GameObject lineTrigger;
        [SerializeField] private GameObject dragTrigger;
        [HideInInspector] public bool IsMoving { get; set; } = false;
        [HideInInspector] public int Index { get; set; }

        private void Awake()
        {
            LineDraggerC = new LineDraggerClass(gameObject, lineTrigger);
            LineAttacherC = new LineAttachClass(gameObject, attachTrigger);
        }
        void Update()
        {
            if (IsMoving)
            {
                LineDraggerC.SetLinesLocation();
                LineAttacherC.SetLineLocation();
            }
        }
        /// <summary>
        /// calls the nodeManager to move this node
        /// </summary>
        public void MoveTrigger() 
        {
            SavedReff.NodeManager.MoveNode(gameObject);
        }
        public void SetDragTriggerActive(bool active)
        {
            dragTrigger.SetActive(active);
        }
        public void DestroyNode()
        {
            LineDraggerC.DeleteLines();
            LineAttacherC.DeleteLine();
            Destroy(gameObject);
        }
        public void StartLine()
        {
            LineDraggerC.StartLine();
            IsMoving = false;
        }
        public void IAttachLine(Line Line)
        {
            LineAttacherC.AttachLine(Line);
            Line.AttachedTo = this;
        }
        public void DeatachLine()
        {
            LineAttacherC.DeatachLine();
        }
    }
}
