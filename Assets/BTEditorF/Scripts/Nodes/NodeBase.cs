using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using System;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public abstract class NodeBase : MonoBehaviour, IObjDestroyable
    {
        [HideInInspector] public NodeDesign NodeD;
        [SerializeField] protected GameObject LineTrigger;
        protected LineHandler lineHandler = new();
        [SerializeField] protected GameObject dragTrigger;
        [SerializeField] protected GameObject attachTrigger;
        public int SaveIndex { get; set; }
        protected SavedNodeBase saveData;
        private static NodeDesign DefaultNodeNd = new("Default","",Color.black,Color.black);
        public void InitializeSave(int index)
        {
            SaveIndex = index;
        }
        public abstract SavedNodeBase Save();
        public virtual void InitializeLoad(SavedNodeBase savedata)  //save data and asign location,index
        {
            saveData = savedata;
            SaveIndex = savedata.NodeIndex;
        }
        public virtual void Load() 
        {
            lineHandler.SpawnedPoint = saveData.SpawnedPointIndex == -1 ? null : SpawnManager.Instance.ActiveLines[saveData.SpawnedPointIndex];
            lineHandler.AttachedPoint = saveData.AtachedPointIndex == -1 ? null : SpawnManager.Instance.ActiveLines[saveData.AtachedPointIndex];
        }

        public virtual void DestroyObject()
        {
            lineHandler.DestroyLineHandler();
            SpawnManager.Instance.RemoveActiveNode(this);
            SpawnManager.Instance.TriggerObjectsUpdated();
            SoundManager.Instance.PlayBaloonPop();
            Destroy(this.gameObject);
        }

        public virtual void InitializeNode(NodeDesign nd)
        {
            NodeD = nd ?? DefaultNodeNd;

        }
        public virtual void InitializeNode(NodeDesign nd,SavedProjectLayer Projectlayer)
        {
            NodeD = nd ?? DefaultNodeNd;
        }
        public void CallSpawnLine()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineHandler.SpawnLine();
            }
        }
        public virtual void AttachLine(LinePoint Line)
        {
            lineHandler.AttachLine(Line);
        }

        public virtual void DeAttachLine()
        {
            lineHandler.DeAttachLine();
        }
    }
}
