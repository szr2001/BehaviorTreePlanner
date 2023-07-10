using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Nodes;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTreePlanner.MenuUi
{
    public class NodesMenu : MonoBehaviour
    {
        [SerializeField] private GameObject NodeHolder;

        [SerializeField] private List<NodeDesign> NodeDesigns = new();
        private List<GameObject> MenuSpawnedNodes = new();

        public List<NodeDesign> Save()
        {
            return NodeDesigns;
        }
        public void Load(List<NodeDesign> nodedesigns)
        {
            NodeDesigns.Clear();
            NodeDesigns = nodedesigns;
            if (NodeDesigns.Count == 0)
            {
                NodeDesigns.Add(new NodeDesign("Selector", "", new Color(0.33f, 1f, 0f), new Color(0.99f, 0.45f, 0.09f)));
                NodeDesigns.Add(new NodeDesign("Sequence", "", new Color(1f, 0.92f, 0f), new Color(0.99f, 0.45f, 0.09f)));
                NodeDesigns.Add(new NodeDesign("Parallel", "", new Color(0.80f, 0.35f, 1f), new Color(0.99f, 0.45f, 0.09f)));
                NodeDesigns.Add(new NodeDesign("Task", "", new Color(0f, 0.93f, 1f), new Color(0.99f, 0.45f, 0.09f)));
            }
            RefreshVisibleNodes();
        }
        public bool ContainsType(string type)
        {
            foreach (NodeDesign nt in NodeDesigns)
            {
                if (type == nt.type)
                {
                    return true;
                }
            }
            return false;
        }
        public void LoadNodeTypes(List<NodeDesign> nodeDesigns)
        {
            this.NodeDesigns = nodeDesigns;
            RefreshVisibleNodes();
        }
        public void AddNewType(NodeDesign nd)
        {
            NodeDesigns.Add(nd);
            RefreshVisibleNodes();
        }
        public void DeleteNode(NodeDesign NodeD)
        {
            foreach (NodeDesign nd in NodeDesigns)
            {
                if (nd.type == NodeD.type)
                {
                    NodeDesigns.Remove(nd);
                    RefreshVisibleNodes();
                    return;
                }
            }
        }
        private void RefreshVisibleNodes()
        {
            ClearButtons();

            if (NodeDesigns.Count > 0)
            {
                foreach (NodeDesign D in NodeDesigns)
                {
                    GameObject SpawnedNode = GameObject.Instantiate(SpawnManager.Instance.NodeButtonPrefabReff);
                    SpawnedNode.GetComponent<NodeButton>().InitializeButton(D, this);
                    SpawnedNode.transform.SetParent(NodeHolder.transform);
                    SpawnedNode.transform.localPosition = new Vector3(SpawnedNode.transform.position.x, SpawnedNode.transform.position.y, 0);
                    SpawnedNode.transform.localScale = Vector3.one;
                    MenuSpawnedNodes.Add(SpawnedNode);
                }
            }
        }
        private void ClearButtons()
        {
            if (MenuSpawnedNodes.Count > 0)
            {
                foreach (GameObject g in MenuSpawnedNodes)
                {
                    Destroy(g);
                }
            }
        }
    }
}
