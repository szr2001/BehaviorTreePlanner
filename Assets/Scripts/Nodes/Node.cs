using UnityEngine;
using UnityEngine.UI;
using BehaviorTreePlanner.SaveGame;

namespace BehaviorTreePlanner.Nodes
{
    public class Node : MovingNode
    {
        [SerializeField] private Image TopImageReff;
        [SerializeField] private Image BotImageReff;
        [SerializeField] private Text TypeText;
        [SerializeField] private InputField NameText;
        public void EditName(string n)
        {
            NameText.text = n;
            NodeD.name = n;
        }
        public override void LoadNode(NodeSaveInfo nodeInfo)
        {

        }
        public override void SetNodeType(NodeDesign nd)
        {
            NodeD = nd;
            TopImageReff.color = NodeD.PrimaryCollor;
            BotImageReff.color = NodeD.SecondaryCollor;
            TypeText.text = NodeD.type;
            if (NodeD.name != "")
            {
                NameText.text = NodeD.name;
            }

        }
    }
}
