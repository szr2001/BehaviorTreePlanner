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

        public List<GameObject> ActiveNodes { get; set; } = new();
        public List<GameObject> ActiveLines { get; set; } = new();

        public void SpawnNode(NodeDesign Nd)
        {
            GameObject TempNode = Instantiate(EditorManager.SpawnManager.NodePrefabReff, EditorManager.SpawnManager.Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "Node";
            TempNode.GetComponent<Node>().InitializeNode(Nd, EditorManager);
            AddActiveNode(TempNode);
            EditorManager.MoveObjectsManager.AddMovableObj(TempNode.GetComponent<MovingNode>());
            EditorManager.MoveObjectsManager.StartMoving();
        }
        public void SpawnLayerNode(SavedProjectLayer layer)
        {
            GameObject TempNode = Instantiate(LayerNodePrefabReff, Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "Node";
            NodeDesign NodeD = new(null, null, new Color(0.57f, 0.3f, 1), Color.white);
            TempNode.GetComponent<LayerNode>().InitializeNode(NodeD,EditorManager,layer);
            AddActiveNode(TempNode);
            EditorManager.MoveObjectsManager.AddMovableObj(TempNode.GetComponent<MovingNode>());//problem? get nodebase or movingnode
            EditorManager.MoveObjectsManager.StartMoving();
        }
        public LinePoint SpawnLinePoint(LinePoint Caller,bool SaveReff,bool IsRoot)
        {
            GameObject TempLine = Instantiate(EditorManager.SpawnManager.LinePointPrefabReff, EditorManager.SpawnManager.Screen.transform);
            TempLine.transform.localScale = new Vector3(1, 1, 1);
            TempLine.name = "LinePoint";
            TempLine.GetComponent<LinePoint>().InitializeLine(Caller, IsRoot,EditorManager);
            if (SaveReff)
            {
                AddActiveLine(TempLine);
                EditorManager.MoveObjectsManager.AddMovableObj(TempLine.GetComponent<LinePoint>());
                EditorManager.MoveObjectsManager.StartMoving();
            }

            return TempLine.GetComponent<LinePoint>();
        }



        public void AddActiveNode(GameObject node)
        {
            if (!ActiveNodes.Contains(node))
            {
                ActiveNodes.Add(node);
            }
        }
        public void RemoveActiveNode(GameObject node)
        {
            if (ActiveNodes.Contains(node))
            {
                ActiveNodes.Remove(node);
            }
        }
        public void AddActiveLine(GameObject line)
        {
            if (!ActiveLines.Contains(line))
            {
                ActiveLines.Add(line);
            }
        }
        public void RemoveActiveLine(GameObject line)
        {
            if (ActiveLines.Contains(line))
            {
                ActiveLines.Remove(line);
            }
        }
    }
}
