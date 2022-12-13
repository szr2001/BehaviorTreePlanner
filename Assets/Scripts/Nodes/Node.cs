using UnityEngine;
using UnityEngine.UI;
using BehaviorTreePlanner.SaveGame;

namespace BehaviorTreePlanner.Nodes
{
    public class Node : MovingNode
    {
        
        [HideInInspector] public NodeType nodeT;
        
        [SerializeField] private Image TopImageReff;
        [SerializeField] private Image BotImageReff;
        [SerializeField] private Text TypeText;
        [SerializeField] private InputField NameText;

        //update every spawned line pos1 to the location of this node
        //update the attached line pos2 to the location of this node

        public void LoadNode(NodeSaveInfo nodeInfo)
        {

        }
        public void EditName(string n)
        {
            NameText.text = n;
            nodeT.name = n;
        }
        public void SetNodeType(NodeType nt)
        {
            nodeT = nt;
            TopImageReff.color = nodeT.topColor;
            BotImageReff.color = nodeT.botColor;
            TypeText.text = nodeT.type;
            if (nodeT.name != "")
            {
                NameText.text = nodeT.name;
            }

        }
    }
}
