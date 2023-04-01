using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class NodeBase : MonoBehaviour
    {
        [HideInInspector] public NodeDesign NodeD;
        public LineDraggerClass LineDraggerC { get; set; }
        [SerializeField] private GameObject lineTrigger;

        protected virtual void Awake()
        {
            LineDraggerC = new LineDraggerClass(gameObject, lineTrigger);
        }
        public virtual void SetNodeType(NodeDesign nd)
        {
            NodeD = nd;
        }
        public virtual void DestroyNode()
        {
            Destroy(gameObject);
        }
    }
}
