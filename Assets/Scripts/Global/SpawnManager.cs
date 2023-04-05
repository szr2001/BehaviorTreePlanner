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
        public void SpawnNode(NodeDesign Nd)
        {
            GameObject TempNode = Instantiate(SavedReff.NodePrefabReff, SavedReff.Screen.transform);
            TempNode.transform.localScale = new Vector3(1, 1, 1);
            TempNode.name = "Node";
            TempNode.GetComponent<Node>().SetNodeType(Nd);
            SavedReff.AddActiveNode(TempNode);
            SavedReff.MoveObjectsManager.AddMovableObj(TempNode.GetComponent<Node>());
            SavedReff.MoveObjectsManager.StartMoving();
        }
        public LinePoint SpawnLinePoint(bool SaveReff,bool IsRoot)
        {
            GameObject TempLine = Instantiate(SavedReff.LinePointPrefabReff, SavedReff.Screen.transform);
            TempLine.transform.localScale = new Vector3(1, 1, 1);
            TempLine.name = "LinePoint";
            if (IsRoot)
            {
                TempLine.GetComponent<LinePoint>().SetRoot();
            }
            if (SaveReff)
            {
                SavedReff.AddActiveLine(TempLine);
                SavedReff.MoveObjectsManager.AddMovableObj(TempLine.GetComponent<LinePoint>());
                SavedReff.MoveObjectsManager.StartMoving();
            }

            return TempLine.GetComponent<LinePoint>();
        }
    }
}
