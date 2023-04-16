using BehaviorTreePlanner.Global;
using UnityEngine;
using UnityEngine.UI;

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
        public override void InitializeNode(NodeDesign nd, EditorManager editormanager)
        {
            base.InitializeNode(nd, editormanager);
            TopImageReff.color = NodeD.PrimaryCollor;
            BotImageReff.color = NodeD.SecondaryCollor;
            TypeText.text = NodeD.type;
            if (NodeD.name != "")
            {
                NameText.text = NodeD.name;
            }
        }
        public override void StartMoveObj()
        {
            NodeHighLight.SetActive(true);
        }

        public override void StopMoveObj()
        {
            NodeHighLight.SetActive(false);
        }
    }
}