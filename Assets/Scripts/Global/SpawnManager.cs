using System.Collections;
using System.Collections.Generic;
using BehaviorTreePlanner.Global;
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
        public void SpawnLinePoint()
        {

        }
    }
}
