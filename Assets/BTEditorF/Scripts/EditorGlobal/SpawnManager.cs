using System.Collections;
using System.Collections.Generic;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using BehaviorTreePlanner.Player;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;
        public CameraControl PlayerControll;
        //move some stufff in the menus like buttons reff
        public GameObject Screen;
        public GameObject NodePrefabReff;
        public GameObject LayerNodePrefabReff;
        public GameObject BlackBoardNodePrefabReff;
        public GameObject LinePointPrefabReff;
        public GameObject NodeButtonPrefabReff;
        public GameObject LayerNodeButtonPrefabReff;

        private readonly Vector3 BlackboardLocation = new(-12.09677f, 908.0645f,0);
        [field:SerializeField] public List<NodeBase> ActiveNodes { get; set; } = new();
        [field: SerializeField] public List<LinePoint> ActiveLines { get; set; } = new();
        [field: SerializeField] public NodeBase ActiveBlackBoard { get; set; }
        //add a bool for start moving and for highlighting the node

        public delegate void SpawnedLine(string LayerName);
        public event SpawnedLine OnObjectsUpdated;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void TriggerObjectsUpdated()
        {
            OnObjectsUpdated?.Invoke(SaveLoadManager.Instance.ActiveProjectLayer.LayerName);
        }
        public NodeBase SpawnNode(NodeDesign Nd, bool StartMoving = true)
        {
            GameObject TempNode = Instantiate(NodePrefabReff, Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "Node";
            NodeBase nodebase = TempNode.GetComponent<NodeBase>();
            nodebase.InitializeNode(Nd);
            AddActiveNode(nodebase);
            if (StartMoving)
            {
                MoveObjectsManager.Instance.AddMovableObj(TempNode.GetComponent<MovingNode>());
                MoveObjectsManager.Instance.StartMoving();
            }
            TriggerObjectsUpdated();
            return nodebase;
        }

        public NodeBase SpawnLayerNode(SavedProjectLayer layer, bool StartMoving = true)
        {
            GameObject TempNode = Instantiate(LayerNodePrefabReff, Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "LayerNode";
            NodeDesign NodeD = new(null, null, new Color(0.57f, 0.3f, 1), Color.white);
            NodeBase nodebase = TempNode.GetComponent<NodeBase>();
            nodebase.InitializeNode(NodeD, layer);
            AddActiveNode(nodebase);
            if (StartMoving)
            {
                MoveObjectsManager.Instance.AddMovableObj(TempNode.GetComponent<MovingNode>());
                MoveObjectsManager.Instance.StartMoving();
            }
            TriggerObjectsUpdated();
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
            nodebase.InitializeNode(NodeD);
            ActiveBlackBoard = nodebase;
            return ActiveBlackBoard;
        }

        public LinePoint SpawnLinePoint(LinePoint Caller,bool SaveReff,bool IsRoot, bool StartMoving = true)
        {
            GameObject TempLine = Instantiate(SpawnManager.Instance.LinePointPrefabReff, SpawnManager.Instance.Screen.transform);
            TempLine.transform.localScale = new Vector3(1, 1, 1);
            TempLine.name = "LinePoint";
            LinePoint linepoint = TempLine.GetComponent<LinePoint>();
            linepoint.InitializeLine(Caller, IsRoot);
            AddActiveLine(linepoint);
            if (SaveReff)
            {
                if (StartMoving)
                {
                    MoveObjectsManager.Instance.AddMovableObj(TempLine.GetComponent<LinePoint>());
                    MoveObjectsManager.Instance.StartMoving();
                }
            }
            TriggerObjectsUpdated();
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
