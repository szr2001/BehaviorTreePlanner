using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public abstract class NodeBase : MonoBehaviour, IObjDestroyable
    {
        protected EditorManager EditorManager;
        [HideInInspector] public NodeDesign NodeD;
        [SerializeField] protected GameObject LineTrigger;
        protected LineHandler lineHandler = new();
        public int SaveIndex { get; set; }
        public abstract SavedNodeBase Save();
        public void AsignUniqueIndex(int index)
        {
            SaveIndex = index;
        }

        public virtual void DestroyObject()
        {
            lineHandler.DestroyLineHandler();
            EditorManager.SpawnManager.RemoveActiveNode(this);
            Destroy(this.gameObject);
        }

        public virtual void InitializeNode(NodeDesign nd,EditorManager editormanager)
        {
            EditorManager = editormanager;
            NodeD = nd;
        }
        public virtual void InitializeNode(NodeDesign nd, EditorManager editormanager,SavedProjectLayer Projectlayer)
        {
            EditorManager = editormanager;
            NodeD = nd;
        }
    }
}
