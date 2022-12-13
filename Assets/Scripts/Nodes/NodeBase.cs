using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
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
            LineDraggerC.StartLine(this,null);
        }
    }
}
