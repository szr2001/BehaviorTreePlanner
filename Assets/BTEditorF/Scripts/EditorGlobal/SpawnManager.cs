using System.Collections;
using System.Collections.Generic;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SpawnManager : MonoBehaviour
    {
        public EditorManager EditorManager;

        //move some stufff in the menus like buttons reff
        public GameObject Screen;
        public GameObject NodePrefabReff;
        public GameObject LayerNodePrefabReff;
        public GameObject BlackBoardNodePrefabReff;
        public GameObject LinePointPrefabReff;
        public GameObject NodeButtonPrefabReff;
        public GameObject LayerNodeButtonPrefabReff;

        private readonly Vector3 BlackboardLocation = new Vector3(-12.09677f, 908.0645f,0);
        [field:SerializeField] public List<NodeBase> ActiveNodes { get; set; } = new();
        [field: SerializeField] public List<LinePoint> ActiveLines { get; set; } = new();
        [field: SerializeField] public NodeBase ActiveBlackBoard { get; set; }
        //add a bool for start moving and for highlighting the node
        public NodeBase SpawnNode(NodeDesign Nd, bool StartMoving = true)
        {
            GameObject TempNode = Instantiate(NodePrefabReff, Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "Node";
            NodeBase nodebase = TempNode.GetComponent<NodeBase>();
            nodebase.InitializeNode(Nd, EditorManager);
            AddActiveNode(nodebase);
            if (StartMoving)
            {
                EditorManager.MoveObjectsManager.AddMovableObj(TempNode.GetComponent<MovingNode>());
                EditorManager.MoveObjectsManager.StartMoving();
            }
            return nodebase;
        }

        public NodeBase SpawnLayerNode(SavedProjectLayer layer, bool StartMoving = true)
        {
            GameObject TempNode = Instantiate(LayerNodePrefabReff, Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "LayerNode";
            NodeDesign NodeD = new(null, null, new Color(0.57f, 0.3f, 1), Color.white);
            NodeBase nodebase = TempNode.GetComponent<NodeBase>();
            nodebase.InitializeNode(NodeD, EditorManager, layer);
            AddActiveNode(nodebase);
            if (StartMoving)
            {
                EditorManager.MoveObjectsManager.AddMovableObj(TempNode.GetComponent<MovingNode>());
                EditorManager.MoveObjectsManager.StartMoving();
            }
            return nodebase;
        }
        public NodeBase SpawnBlackBoardNode()
        {
            if(ActiveBlackBoard != null)
            {
                ActiveBlackBoard.GetComponent<IObjDestroyable>().DestroyObject();
            }
            GameObject TempNode = Instantiate(BlackBoardNodePrefabReff, Screen.transform);
            TempNode.transform.localPosition = BlackboardLocation;
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "BlackBoard";
            NodeDesign NodeD = new(null, null, new Color(0.57f, 0.3f, 1), Color.white);
            NodeBase nodebase = TempNode.GetComponent<NodeBase>();
            nodebase.InitializeNode(NodeD, EditorManager);
            ActiveBlackBoard = nodebase;
            return ActiveBlackBoard;
        }
        public LinePoint SpawnLinePoint(LinePoint Caller,bool SaveReff,bool IsRoot, bool StartMoving = true)
        {
            GameObject TempLine = Instantiate(EditorManager.SpawnManager.LinePointPrefabReff, EditorManager.SpawnManager.Screen.transform);
            TempLine.transform.localScale = new Vector3(1, 1, 1);
            TempLine.name = "LinePoint";
            LinePoint linepoint = TempLine.GetComponent<LinePoint>();
            linepoint.InitializeLine(Caller, IsRoot, EditorManager);
            AddActiveLine(linepoint);//it was in if
            if (SaveReff)
            {
                if (StartMoving)
                {
                    EditorManager.MoveObjectsManager.AddMovableObj(TempLine.GetComponent<LinePoint>());
                    EditorManager.MoveObjectsManager.StartMoving();
                }
            }
            return linepoint;
        }

        public void AddActiveNode(NodeBase node)
        {
            if (!ActiveNodes.Contains(node))
            {
                ActiveNodes.Add(node);
            }
        }

        public void RemoveActiveNode(NodeBase node)
        {
            if (ActiveNodes.Contains(node))
            {
                ActiveNodes.Remove(node);
            }
        }

        public void AddActiveLine(LinePoint line)
        {
            if (!ActiveLines.Contains(line))
            {
                ActiveLines.Add(line);
            }
        }

        public void RemoveActiveLine(LinePoint line)
        {
            if (ActiveLines.Contains(line))
            {
                ActiveLines.Remove(line);
            }
        }
    }
}
