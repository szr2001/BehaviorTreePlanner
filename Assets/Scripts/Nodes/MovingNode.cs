using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner.Nodes
{
    public class MovingNode : NodeBase,IMovable,IAtachLine
    {
        [field:SerializeField]public GameObject NodeHighLight { get; set; }
        [SerializeField] private GameObject dragTrigger;
        [SerializeField] private GameObject attachTrigger;

        public Vector3 GetStartPosition { get { return gameObject.transform.position;}}

        public void MoveObj(Vector3 newPos, Vector3 Offset)
        {
            Vector2 GridSize = SavedSettings.NodeGridSize;
            Vector3 activeNodePos = SavedReff.MousePositionToGrid(newPos, GridSize, Offset);
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
        public void CallSpawnLine()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SavedReff.SpawnManager.SpawnLinePoint(this.gameObject);
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
