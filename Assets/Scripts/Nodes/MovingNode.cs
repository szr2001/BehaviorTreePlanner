using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Interfaces;
using BehaviorTreePlanner.Lines;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner.Nodes
{
    public class MovingNode : NodeBase,IAttachLine,IMovable
    {
        [field:SerializeField]public GameObject NodeHighLight { get; set; }
        [SerializeField] private GameObject dragTrigger;
        [SerializeField] private GameObject attachTrigger;
        public LineAttachClass LineAttacherC { get; set; }
        protected override void Awake()
        {
            LineAttacherC = new LineAttachClass(gameObject, attachTrigger);
            base.Awake();
        }
        public void MoveObj(Vector3 newPos)
        {
            Vector2 GridSize = SavedSettings.NodeGridSize;
            Vector3 activeNodePos = SavedReff.MousePositionToGrid(newPos, GridSize, Vector3.zero);
            RaycastHit2D hit = Physics2D.Raycast(activeNodePos, -Vector2.zero);
            if (!hit)
            { 
                gameObject.transform.position = activeNodePos;
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
            }
        }
        public void SetMoveNode()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SavedReff.MoveObjectsManager.AddMovableObj(this);
                SavedReff.MoveObjectsManager.StartMoving();
            }
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
