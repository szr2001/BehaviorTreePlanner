using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class NodeBase : MonoBehaviour
    {
        [HideInInspector] public NodeDesign NodeD;
        [SerializeField] private GameObject lineTrigger;

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
