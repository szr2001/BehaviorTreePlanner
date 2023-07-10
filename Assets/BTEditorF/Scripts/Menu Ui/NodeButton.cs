using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Nodes;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner.MenuUi
{
    public class NodeButton : MonoBehaviour
    {//edit this to spawn with a reff to the NodesUiMenu and move methods inside it and call them from the node
        private NodesMenu nodesMenu;
        [SerializeField] private Image TopImageReff;
        [SerializeField] private Image BotImageReff;
        [SerializeField] private Text TypeText;
        [SerializeField] private GameObject spawnTrigger;
        [SerializeField] private GameObject deleteCustomTypeTrigger;
        [HideInInspector] public NodeDesign NodeD { get; set; }

        public void InitializeButton(NodeDesign nd, NodesMenu nodesmenu)
        {
            NodeD = nd;
            nodesMenu = nodesmenu;
            TopImageReff.color = NodeD.PrimaryCollor;
            BotImageReff.color = NodeD.SecondaryCollor;
            TypeText.text = NodeD.type;
        }
        public void CallSpawnNode()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveObjectsManager.Instance.ClearMovableObj();
                NodeDesign newDesign = new(NodeD.type, NodeD.name, NodeD.PrimaryCollor, NodeD.SecondaryCollor);
                SpawnManager.Instance.SpawnNode(newDesign);
            }
        }
        public void DeleteNode()
        {
            nodesMenu.DeleteNode(NodeD);
        }
    }
}