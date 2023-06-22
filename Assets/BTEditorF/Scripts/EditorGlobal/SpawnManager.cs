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
        public GameObject LinePointPrefabReff;
        public GameObject NodeButtonPrefabReff;
        public GameObject LayerNodeButtonPrefabReff;

        [field:SerializeField] public List<NodeBase> ActiveNodes { get; set; } = new();
        [field: SerializeField] public List<LinePoint> ActiveLines { get; set; } = new();

        public NodeBase SpawnNode(NodeDesign Nd)
        {
            GameObject TempNode = Instantiate(NodePrefabReff, Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "Node";
            NodeBase nodebase = TempNode.GetComponent<NodeBase>();
            nodebase.InitializeNode(Nd, EditorManager);
            AddActiveNode(nodebase);
            EditorManager.MoveObjectsManager.AddMovableObj(TempNode.GetComponent<MovingNode>());
            EditorManager.MoveObjectsManager.StartMoving();
            return nodebase;
        }

        public NodeBase SpawnLayerNode(SavedProjectLayer layer)
        {
            GameObject TempNode = Instantiate(LayerNodePrefabReff, Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "Node";
            NodeDesign NodeD = new(null, null, new Color(0.57f, 0.3f, 1), Color.white);
            NodeBase nodebase = TempNode.GetComponent<NodeBase>();
            nodebase.InitializeNode(NodeD, EditorManager, layer);
            AddActiveNode(nodebase);
            EditorManager.MoveObjectsManager.AddMovableObj(TempNode.GetComponent<MovingNode>());
            EditorManager.MoveObjectsManager.StartMoving();
            return nodebase;
        }

        public LinePoint SpawnLinePoint(LinePoint Caller,bool SaveReff,bool IsRoot)
        {
            GameObject TempLine = Instantiate(EditorManager.SpawnManager.LinePointPrefabReff, EditorManager.SpawnManager.Screen.transform);
            TempLine.transform.localScale = new Vector3(1, 1, 1);
            TempLine.name = "LinePoint";
            LinePoint linepoint = TempLine.GetComponent<LinePoint>();
            linepoint.InitializeLine(Caller, IsRoot, EditorManager);
            if (SaveReff)
            {
                AddActiveLine(linepoint);
                EditorManager.MoveObjectsManager.AddMovableObj(TempLine.GetComponent<LinePoint>());
                EditorManager.MoveObjectsManager.StartMoving();
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
