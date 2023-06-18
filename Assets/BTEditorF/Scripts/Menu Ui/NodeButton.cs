using UnityEngine;
using BehaviorTreePlanner.Nodes;
using UnityEngine.UI;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;

namespace BehaviorTreePlanner.MenuUi
{
    public class NodeButton : MonoBehaviour
    {//edit this to spawn with a reff to the NodesUiMenu and move methods inside it and call them from the node
        private EditorManager EditorManager;
        private NodesMenu nodesMenu;
        [SerializeField] private Image TopImageReff;
        [SerializeField] private Image BotImageReff;
        [SerializeField] private Text TypeText;
        [SerializeField] private GameObject spawnTrigger;
        [SerializeField] private GameObject deleteCustomTypeTrigger;
        [HideInInspector] public NodeDesign NodeD { get; set; }

        public void InitializeButton(NodeDesign nd,EditorManager editormanager,NodesMenu nodesmenu)
        {
            EditorManager = editormanager;
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
                EditorManager.MoveObjectsManager.ClearMovableObj();
                EditorManager.SpawnManager.SpawnNode(NodeD);
            }
        }
        public void DeleteNode()
        {
            nodesMenu.DeleteNode(NodeD);
        }
    }
}