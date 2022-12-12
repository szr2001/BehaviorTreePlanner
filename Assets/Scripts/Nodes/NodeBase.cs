using BehaviorTreePlanner.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTreePlanner.Interfaces;
using BehaviorTreePlanner.Lines;

namespace BehaviorTreePlanner
{
    public class NodeBase : MonoBehaviour
    {
        public LineDraggerClass LineDraggerC { get; set; }
        [SerializeField] private GameObject lineTrigger;

        protected virtual void Awake()
        {
            LineDraggerC = new LineDraggerClass(gameObject, lineTrigger);
        }

        public void MoveTrigger() 
        {
            SavedReff.NodeManager.MoveNode(gameObject);
        }
        public virtual void DestroyNode()
        {
            LineDraggerC.DeleteLines();
            Destroy(gameObject);
        }
        public virtual void StartLine()
        {
            LineDraggerC.StartLine(this);
        }
    }
}
