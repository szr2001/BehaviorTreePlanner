using BehaviorTreePlanner.Global;
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
        public void SpawnNode(GameObject node , GameObject attachedLine)
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition), -Vector2.zero);
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
            MouseOffset = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition) - activeNodeCopy.transform.position;
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
                    activeNodeComp.SetDragTriggerActive(true); // adauga bool in global reff ca daca dai click pe buton in timp[ ce controlezi asta spawnezi 2 node
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
            if (SavedSettings.EnableSnapToGrid)
            {
                Vector2 mospos = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
                Vector3 activeNodePos = new Vector3(mospos.x, mospos.y, 0) - MouseOffset;
                activeNodePos.x = Mathf.Round(activeNodePos.x);
                activeNodePos.y = Mathf.Round(activeNodePos.y);
                if (activeNodePos.x % 1 == 0 && activeNodePos.y % 1 == 0)
                {
                    RaycastHit2D hit = Physics2D.Raycast(activeNodePos, -Vector2.zero);
                    if (!hit || hit.collider.gameObject.CompareTag("NodeButton"))
                    {
                        activeNodeCopy.transform.position = activeNodePos;
                    }
                }
            }
            else //bug when is not snaptogrid, disable moved node collider
            {
                Vector2 mospos = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mospos, -Vector2.zero);
                if (!hit || hit.collider.gameObject.CompareTag("NodeButton"))
                {
                    activeNodeCopy.transform.position = new Vector3(mospos.x, mospos.y, 0) - MouseOffset;
                }
            }
        }
    }
}