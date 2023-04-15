using UnityEngine;
using BehaviorTreePlanner.Nodes;
using UnityEngine.UI;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;

namespace BehaviorTreePlanner.MenuUi
{
    public class NodeButton : MonoBehaviour
    {
        private EditorManager EditorManager;
        [SerializeField] private Image TopImageReff;
        [SerializeField] private Image BotImageReff;
        [SerializeField] private Text TypeText;
        [SerializeField] private GameObject spawnTrigger;
        [SerializeField] private GameObject deleteCustomTypeTrigger;
        [HideInInspector] public NodeDesign NodeD { get; set; }

        public void InitializeButton(NodeDesign nd,EditorManager editormanager)
        {
            EditorManager = editormanager;
            NodeD = nd;
            TopImageReff.color = NodeD.PrimaryCollor;
            BotImageReff.color = NodeD.SecondaryCollor;
            TypeText.text = NodeD.type;
        }
        public void SpawnNodeCall()
        {
            if (Input.GetMouseButtonDown(0))
            {
                EditorManager.MoveObjectsManager.ClearMovableObj();
                EditorManager.SpawnManager.SpawnNode(NodeD);
            }
        }
        public void DeleteNode()
        {
            EditorManager.NodesUiMenu.GetComponent<NodesMenu>().DeleteNode(NodeD);
        }
    }
}