using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner.Nodes
{
    public class NodeManager : MonoBehaviour
    {
        private GameObject activeNodeCopy;
        private bool IsMovingSelectedNode = false; 
        private bool IsMovingSpawnedNode = false; 
        private Vector3 MouseOffset = new Vector3(0, 0.3f, 0);
        void Update()
        {
            MoveSelectedNode();
            MoveSpawnedNode();
        }
        public void SpawnNode(GameObject node , Line attachedLine)
        {
            IsMovingSelectedNode = false;
            activeNodeCopy = GameObject.Instantiate(node);
            activeNodeCopy.name = node.name;
            activeNodeCopy.transform.SetParent(SavedReff.Screen.transform);
            activeNodeCopy.transform.localScale = new Vector3(1, 1, 1);
            MouseOffset = new Vector3(0, 0.3f, 0);
            IsMovingSpawnedNode = true;
            activeNodeCopy.GetComponent<Node>().IAttachLine(attachedLine);
        }
        public void SpawnNode(GameObject node)
        {
            RaycastHit2D hit = Physics2D.Raycast(SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition), -Vector2.zero);
            if (!hit || hit.collider.gameObject.CompareTag("NodeButton"))
            {
                IsMovingSelectedNode = false;
                activeNodeCopy = GameObject.Instantiate(node);
                activeNodeCopy.name = node.name;
                activeNodeCopy.transform.SetParent(SavedReff.Screen.transform);
                activeNodeCopy.transform.localScale = new Vector3(1, 1, 1);
                MouseOffset = new Vector3(0, 0.3f, 0);
                IsMovingSpawnedNode = true;
                node.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        public void MoveNode(GameObject node)
        {
            IsMovingSpawnedNode = false;
            activeNodeCopy = node;
            MouseOffset = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition) - activeNodeCopy.transform.position;
            MouseOffset.z = 0;
            IsMovingSelectedNode = true;
        }
        private void MoveSelectedNode()
        {
            if (activeNodeCopy != null && IsMovingSelectedNode)
            {
                NodeBase activeNodeComp = activeNodeCopy.GetComponent<NodeBase>();
                if (Input.GetMouseButton(0))
                {
                    activeNodeComp.SetDragTriggerActive(false);
                    activeNodeComp.IsMoving = (true);
                    MoveLogic();
                    if (Input.GetMouseButtonDown(1))
                    {
                        activeNodeComp.DestroyNode();
                        activeNodeCopy = null;
                    }
                }
                else
                {
                    activeNodeComp.IsMoving = (false);
                    activeNodeComp.SetDragTriggerActive(true);
                    activeNodeCopy = null;
                    IsMovingSelectedNode = false;
                }
            }
        }
        private void MoveSpawnedNode()
        {
            if (activeNodeCopy != null && IsMovingSpawnedNode)
            {
                SavedReff.IsSpawningNodes = true;
                NodeBase activeNodeComp = activeNodeCopy.GetComponent<NodeBase>();
                activeNodeComp.SetDragTriggerActive(false);
                activeNodeComp.IsMoving = (true);
                MoveLogic();
                if (Input.GetMouseButtonDown(00))
                {
                    activeNodeComp.IsMoving = (false);
                    activeNodeComp.SetDragTriggerActive(true);
                    SpawnNode(activeNodeCopy);
                }
                //destroy the node when right click
                if (Input.GetMouseButtonDown(01))
                {
                    SavedReff.IsSpawningNodes = false;
                    activeNodeComp.DestroyNode();
                    activeNodeCopy = null;
                    IsMovingSpawnedNode = false;
                }
            }
        }
        private void MoveLogic() 
        {
            string NodeButtonTag = "NodeButton";
            if (SavedSettings.EnableSnapToGrid)
            {
                Vector2 mospos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                Vector3 activeNodePos = new Vector3(mospos.x, mospos.y, 0) - MouseOffset;
                activeNodePos.x = Mathf.Round(activeNodePos.x);
                activeNodePos.y = Mathf.Round(activeNodePos.y);
                if (activeNodePos.x % 1 == 0 && activeNodePos.y % 1 == 0)
                {
                    RaycastHit2D hit = Physics2D.Raycast(activeNodePos, -Vector2.zero);
                    if (!hit || hit.collider.gameObject.CompareTag(NodeButtonTag))
                    {
                        activeNodeCopy.transform.position = activeNodePos;
                    }
                }
            }
            else //Raycast will hit the node under the mouse and will not move,it looks like its teleporting,disable moved node collider
            {
                Vector2 mospos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mospos, -Vector2.zero);
                if (!hit || hit.collider.gameObject.CompareTag(NodeButtonTag))
                {
                    activeNodeCopy.transform.position = new Vector3(mospos.x, mospos.y, 0) - MouseOffset;
                }
            }
        }
    }
}