using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class NodeBase : MonoBehaviour, IObjDestroyable
    {
        [HideInInspector] public NodeDesign NodeD;
        [SerializeField] protected GameObject LineTrigger;
        protected LineHandler lineHandler = new();

        public virtual void DestroyObject()
        {
            lineHandler.DestroyLineHandler();
            SavedReff.RemoveActiveNode(this.gameObject);
            Destroy(this.gameObject);
        }

        public virtual void SetNodeType(NodeDesign nd)
        {
            NodeD = nd;
        }
    }
}
