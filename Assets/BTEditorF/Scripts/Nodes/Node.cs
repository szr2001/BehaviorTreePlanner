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

        public override SavedNodeBase Save()
        {
            float[] nodepos = new float[]{transform.position.x,transform.position.y,transform.position.z};
            return new SavedNode
                (
                    nodepos,
                    NodeD,
                    SaveIndex,
                    lineHandler.AttachedPoint != null ? lineHandler.AttachedPoint.SaveIndex : -1 ,
                    lineHandler.SpawnedPoint != null ? lineHandler.SpawnedPoint.SaveIndex : -1
                );
        }
        public override void InitializeLoad(SavedNodeBase savedata)
        {
            base.InitializeLoad(savedata);
            InitializeNode(NodeD);
        }

        public override void Load()
        {
            base.Load();
        }
        public void EditName(string n)
        {
            NameText.text = n;
            NodeD.name = n;
        }
        public override void InitializeNode(NodeDesign nd)
        {
            base.InitializeNode(nd);
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
