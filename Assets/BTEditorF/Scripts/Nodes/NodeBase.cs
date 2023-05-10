using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class NodeBase : MonoBehaviour, IObjDestroyable
    {
        protected EditorManager EditorManager;
        [HideInInspector] public NodeDesign NodeD;
        [SerializeField] protected GameObject LineTrigger;
        protected LineHandler lineHandler = new();

        public virtual void DestroyObject()
        {
            lineHandler.DestroyLineHandler();
            EditorManager.SpawnManager.RemoveActiveNode(this.gameObject);
            Destroy(this.gameObject);
        }

        public virtual void InitializeNode(NodeDesign nd,EditorManager editormanager)
        {
            EditorManager = editormanager;
            NodeD = nd;
        }
    }
}
