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
        public GameObject GetGameObj { get { return gameObject; } }
        public Vector3 GetObjPosition { get { return gameObject.transform.position;}}

        private void Awake()
        {
            lineHandler.InitializeLineHandler(this,attachTrigger.transform, LineTrigger.transform);
        }
        public void MoveObj(Vector3 newPos, Vector3 Offset, bool UseGrid)
        {
            Vector2 GridSize = SavedSettings.NodeGridSize;
            Vector3 activeNodePos = UseGrid ? SavedReff.MousePositionToGrid(newPos, GridSize, Offset,Vector2.zero) : newPos;
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
        public virtual void StartMoveObj()
        {

        }
        public virtual void StopMoveObj()
        {

        }
        public virtual void AttachLine(LinePoint Line)
        {
            lineHandler.AttachLine(Line);
        }
        public void DeAttachLine()
        {
            lineHandler.DeAttachLine();
        }
    }
}
