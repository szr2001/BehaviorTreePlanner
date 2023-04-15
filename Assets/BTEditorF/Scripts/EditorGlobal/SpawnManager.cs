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
        public void SpawnNode(NodeDesign Nd)
        {
            GameObject TempNode = Instantiate(EditorManager.NodePrefabReff, EditorManager.Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "Node";
            TempNode.GetComponent<Node>().InitializeNode(Nd, EditorManager);
            EditorManager.AddActiveNode(TempNode);
            EditorManager.MoveObjectsManager.AddMovableObj(TempNode.GetComponent<Node>());
            EditorManager.MoveObjectsManager.StartMoving();
        }
        public LinePoint SpawnLinePoint(LinePoint Caller,bool SaveReff,bool IsRoot)
        {
            GameObject TempLine = Instantiate(EditorManager.LinePointPrefabReff, EditorManager.Screen.transform);
            TempLine.transform.localScale = new Vector3(1, 1, 1);
            TempLine.name = "LinePoint";
            TempLine.GetComponent<LinePoint>().InitializeLine(Caller, IsRoot,EditorManager);
            if (SaveReff)
            {
                EditorManager.AddActiveLine(TempLine);
                EditorManager.MoveObjectsManager.AddMovableObj(TempLine.GetComponent<LinePoint>());
                EditorManager.MoveObjectsManager.StartMoving();
            }

            return TempLine.GetComponent<LinePoint>();
        }
    }
}
