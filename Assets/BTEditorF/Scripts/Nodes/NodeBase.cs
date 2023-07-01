using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using System;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public abstract class NodeBase : MonoBehaviour, IObjDestroyable
    {
        protected EditorManager editorManager;
        [HideInInspector] public NodeDesign NodeD;
        [SerializeField] protected GameObject LineTrigger;
        protected LineHandler lineHandler = new();
        public int SaveIndex { get; set; }
        protected SavedNodeBase saveData;
        private static NodeDesign DefaultNodeNd = new("Default","",Color.black,Color.black);
        public void InitializeSave(int index)
        {
            SaveIndex = index;
        }
        public abstract SavedNodeBase Save();
        public virtual void InitializeLoad(SavedNodeBase savedata,EditorManager editormanager)  //save data and asign location,index
        {
            editorManager = editormanager;
            saveData = savedata;
            SaveIndex = savedata.NodeIndex;
        }
        public virtual void Load() 
        {
            lineHandler.SpawnedPoint = saveData.SpawnedPointIndex == -1 ? null : editorManager.SpawnManager.ActiveLines[saveData.SpawnedPointIndex];
            lineHandler.AttachedPoint = saveData.AtachedPointIndex == -1 ? null : editorManager.SpawnManager.ActiveLines[saveData.AtachedPointIndex];
        }

        public virtual void DestroyObject()
        {
            lineHandler.DestroyLineHandler();
            editorManager.SpawnManager.RemoveActiveNode(this);
            Destroy(this.gameObject);
        }

        public virtual void InitializeNode(NodeDesign nd,EditorManager editormanager)
        {
            editorManager = editormanager;
            NodeD = nd ?? DefaultNodeNd;

        }
        public virtual void InitializeNode(NodeDesign nd, EditorManager editormanager,SavedProjectLayer Projectlayer)
        {
            editorManager = editormanager;
            NodeD = nd ?? DefaultNodeNd;
        }
        public void CallSpawnLine()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineHandler.SpawnLine();
            }
        }
        public void AttachLine(LinePoint Line)
        {
            lineHandler.AttachLine(Line);
        }

        public void DeAttachLine()
        {
            lineHandler.DeAttachLine();
        }
    }
}
