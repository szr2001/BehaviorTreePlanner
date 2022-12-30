using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.SaveGame;
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
        public virtual void LoadNode(NodeSaveInfo nodeInfo)
        {

        }
        public virtual void SetNodeType(NodeDesign nd)
        {
            NodeD = nd;
        }
        public virtual void DestroyNode()
        {
            LineDraggerC.DeleteLines();
            Destroy(gameObject);
        }
        public virtual void StartLine()
        {
            LineDraggerC.StartLine(this,null,NodeD.PrimaryCollor);
        }
    }
}
