using UnityEngine;
using UnityEngine.UI;
using BehaviorTreePlanner.Nodes;
using BehaviorTreePlanner.Global;

namespace BehaviorTreePlanner.MenuUi
{
    public class CreateNodeMenu : MonoBehaviour
    {
        [SerializeField] private EditorManager EditorManager;
        [SerializeField] private Image topNode;
        [SerializeField] private Image botNode;
        [SerializeField] private GameObject colorPicker;
        [SerializeField] private Text nodeType;
        [SerializeField] private GameObject NodeListMenu;
        [SerializeField] private InputField PreviewNodeInputField;

        private bool isOpened = false;
        private CreateNodeColorPicker NodeColorPicker;
        void Start()
        {
            NodeColorPicker = colorPicker.GetComponent<CreateNodeColorPicker>();
        }

        void Update()
        {

        }
        public void NAddNode()
        {
            if (nodeType.text != "Node Type" && !nodeType.text.StartsWith(" ") && nodeType.text != "")
            {
                if (!EditorManager.NodesUiMenu.GetComponent<NodesMenu>().ContainsType(nodeType.text))
                {
                    NodeDesign NewNt = new NodeDesign(nodeType.text, "", topNode.color, botNode.color);
                    NodeListMenu.GetComponent<NodesMenu>().AddNewType(NewNt);
                    ResetNode();
                }
            }
        }
        public void ResetNode()
        {
            topNode.color = new Color(1f, 0.92f, 0f);
            botNode.color = new Color(0.99f, 0.45f, 0.09f);
            PreviewNodeInputField.text = "";
            nodeType.text = "Node Type";
            NodeColorPicker.DisableColorPicker();
            ToggleCreateNodeMenu();
        }

        public void ChangeType(string t)
        {
            nodeType.text = t;
        }
        public void ToggleCreateNodeMenu()
        {
            if (isOpened)
            {
                isOpened = false;
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                isOpened = true;
            }
        }
    }
}