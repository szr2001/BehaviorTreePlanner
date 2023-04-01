using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Interfaces;
using BehaviorTreePlanner.Lines;
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
        public void SetDragTriggerActive(bool active)
        {
            dragTrigger.SetActive(active);
        }
        public void MoveObj(Vector3 newPos)
        {
            gameObject.transform.position = newPos;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
        }

        public virtual void Select()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Deselect()
        {
            throw new System.NotImplementedException();
        }

        public void IAttachLine(LinePoint Line)
        {
            throw new System.NotImplementedException();
        }
    }
}
