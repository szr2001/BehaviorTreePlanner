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
        [HideInInspector] public NodeDesign NodeD { get; set; }
       
        public void SetNodeType(NodeDesign nd)
        {
            NodeD = nd;
            TopImageReff.color = NodeD.PrimaryCollor;
            BotImageReff.color = NodeD.SecondaryCollor;
            TypeText.text = NodeD.type;
        }
        public void SpawnNodeCall()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SavedReff.SpawnManager.SpawnNode(NodeD);
            }
        }
        public void DeleteNode()
        {
            SavedReff.NodesUiMenu.GetComponent<NodesMenu>().DeleteNode(NodeD);
        }

        public void IAttachLine(LinePoint Line)
        {
            throw new System.NotImplementedException();
        }
    }
}