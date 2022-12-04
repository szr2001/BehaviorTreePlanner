using System.Collections.Generic;
using UnityEngine;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Nodes;


namespace BehaviorTreePlanner.MenuUi
{
    public class NodesMenu : MonoBehaviour
    {
        [SerializeField] private GameObject NodeHolder;

        private List<NodeType> Nodetypes = new List<NodeType>();
        private List<GameObject> MenuSpawnedNodes = new List<GameObject>();
        void Start()
        {
            Nodetypes.Add(new NodeType("Selector", "", new Color(0.33f, 1f, 0f), new Color(0.99f, 0.45f, 0.09f)));
            Nodetypes.Add(new NodeType("Sequence", "", new Color(1f, 0.92f, 0f), new Color(0.99f, 0.45f, 0.09f)));
            Nodetypes.Add(new NodeType("Parallel", "", new Color(0.80f, 0.35f, 1f), new Color(0.99f, 0.45f, 0.09f)));
            Nodetypes.Add(new NodeType("Task", "", new Color(0f, 0.93f, 1f), new Color(0.99f, 0.45f, 0.09f)));
            RefreshVisibleNodes();
        }
        // Update is called once per frame
        void Update()
        {

        }
        /// <summary>
        /// Check if a node type exists
        /// </summary>
        public bool ContainsType(string type)
        {
            foreach(NodeType nt in Nodetypes)
            {
                if (type == nt.type)
                {
                    return true;
                }
            }
            return false;
        }
        public void loadNodeTypes(List<NodeType> nodetypes)
        {
            this.Nodetypes = nodetypes;
            RefreshVisibleNodes();
        }
        public void AddNewType(NodeType nt)
        {
            Nodetypes.Add(nt);
            RefreshVisibleNodes();
        }
        public void DeleteNode(NodeType NodeT)
        {
            foreach(NodeType nt in Nodetypes)
            {
                if(nt.type == NodeT.type)
                {
                    Nodetypes.Remove(nt);
                    RefreshVisibleNodes();
                    return;
                }
            }
        }
        public void RefreshVisibleNodes()
        {
            if (MenuSpawnedNodes.Count > 0)
            {
                foreach (GameObject g in MenuSpawnedNodes)
                {
                    Destroy(g);
                }
            }
            if (Nodetypes.Count > 0)
            {
                foreach (NodeType T in Nodetypes)
                {
                    GameObject Node = GameObject.Instantiate(SavedReff.NodeButtonPrefabReff);
                    Node.GetComponent<NodeButton>().SetNodeType(T);
                    Node.transform.SetParent(NodeHolder.transform); 
                    Node.transform.localScale = Vector3.one;
                    MenuSpawnedNodes.Add(Node);
                }
            }
        }
    }
}
