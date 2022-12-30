using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Interfaces;
using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.SaveGame;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner.Nodes
{
    public class MovingNode : NodeBase,IAttachLine,IMovable,ISelectable
    {
        [field:SerializeField]public GameObject NodeHighLight { get; set; }
        public LineAttachClass LineAttacherC { get; set; }
        [SerializeField] private GameObject dragTrigger;
        [SerializeField] private GameObject attachTrigger;
        [HideInInspector] public bool IsMoving { get; set; } = false;
        [HideInInspector] public int Index { get; set; }
        protected override void Awake()
        {
            LineAttacherC = new LineAttachClass(gameObject, attachTrigger);
            base.Awake();
        }
        public override void LoadNode(NodeSaveInfo nodeInfo)
        {
            
        }
        public void UpdateLineLocation()
        {
            LineDraggerC.SetLinesLocation();
            LineAttacherC.SetLineLocation();
        }
        public void MoveTrigger()
        {
            if (!SavedReff.IsMovingSelection)
            {
                SavedReff.NodeManager.MoveNode(gameObject);
            }
        }
        private void Update()
        {
            if (IsMoving)
            {
                UpdateLineLocation();
            }
        }
        public void SetDragTriggerActive(bool active)
        {
            dragTrigger.SetActive(active);
        }
        public override void DestroyNode()
        {
            LineAttacherC.DeleteLine();
            base.DestroyNode();
        }
        public override void StartLine()
        {
            IsMoving = false;
            base.StartLine();
        }
        public void IAttachLine(Line Line)
        {
            if(Line.NodeRoot != this)
            {
                LineAttacherC.AttachLine(Line);
                Line.AttachedToNodeReff = this;
            }
            else
            {
                Line.DestroyLine();
            }
        }
        public void DeatachLine()
        {
            LineAttacherC.DeatachLine();
        }

        public void MoveObj(Vector3 newPos)
        {
            gameObject.transform.position = newPos;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
            UpdateLineLocation();
        }

        public virtual void Select()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Deselect()
        {
            throw new System.NotImplementedException();
        }
    }
}
