using UnityEngine;
using BehaviorTreePlanner.Nodes;
using UnityEngine.UI;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Interfaces;
using BehaviorTreePlanner.Lines;

namespace BehaviorTreePlanner.MenuUi
{
    public class NodeButton : MonoBehaviour,IAttachLine
    {
        [SerializeField] private Image TopImageReff;
        [SerializeField] private Image BotImageReff;
        [SerializeField] private Text TypeText;
        [SerializeField] private GameObject spawnTrigger;
        [SerializeField] private GameObject deleteCustomTypeTrigger;
        [HideInInspector] private Line AttachedLineOnSpawn;
        [HideInInspector] public NodeType NodeT { get; set; }
        /// <summary>
        /// Create a new node and calls the right method from the manager to spawn it depending if there is a line to be attached
        /// </summary>
        public void SetSpawnNode()
        {
            if (!SavedReff.IsSpawningNodes)
            {
                GameObject SpawnedNode = GameObject.Instantiate(SavedReff.NodePrefabReff);
                SpawnedNode.GetComponent<Node>().SetNodeType(NodeT);
                SpawnedNode.GetComponent<BoxCollider2D>().enabled = false;
                if (AttachedLineOnSpawn != null)
                {
                    SavedReff.NodeManager.SpawnNode(SpawnedNode, AttachedLineOnSpawn);
                    AttachedLineOnSpawn = null;
                }
                else
                {
                    SavedReff.NodeManager.SpawnNode(SpawnedNode);
                }
                Destroy(SpawnedNode);
            }
        }
        public void SetNodeType(NodeType nt)
        {
            NodeT = nt;
            TopImageReff.color = NodeT.topColor;
            BotImageReff.color = NodeT.botColor;
            TypeText.text = NodeT.type;
        }
        public void DeleteNode()
        {
            SavedReff.NodesUiMenu.GetComponent<NodesMenu>().DeleteNode(NodeT);
        }
        public void IAttachLine(Line Line)
        {
            AttachedLineOnSpawn = Line;
        }
    }
}