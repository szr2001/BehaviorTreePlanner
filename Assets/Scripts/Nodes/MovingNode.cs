using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner.Nodes
{
    public class MovingNode : NodeBase,IMovable,IAtachLine
    {
        [field:SerializeField]public GameObject NodeHighLight { get; set; }
        [SerializeField] protected GameObject dragTrigger;
        [SerializeField] protected GameObject attachTrigger;

        public Vector3 GetStartPosition { get { return gameObject.transform.position;}}

        private void Awake()
        {
            lineHandler.InitializeLineHandler(attachTrigger.transform, LineTrigger.transform);
        }
        public void MoveObj(Vector3 newPos, Vector3 Offset, bool UseGrid)
        {
            Vector2 GridSize = SavedSettings.NodeGridSize;
            Vector3 activeNodePos = UseGrid ? SavedReff.MousePositionToGrid(newPos, GridSize, Offset) : newPos;
            RaycastHit2D hit = Physics2D.Raycast(activeNodePos, -Vector2.zero);
            if (!hit)
            {
                gameObject.transform.position = activeNodePos;
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
            }
            lineHandler.MoveObj(newPos, Offset, UseGrid);
        }
        public void SetMoveNode()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SavedReff.MoveObjectsManager.AddMovableObj(this);
                SavedReff.MoveObjectsManager.StartMoving();
            }
        }
        public void CallSpawnLine()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineHandler.SpawnLine();
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
        public virtual void AtachLine(LinePoint Line)
        {
            throw new System.NotImplementedException();
        }
    }
}
