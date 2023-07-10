using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class MovingNode : NodeBase, IMovable
    {
        [field: SerializeField] public GameObject NodeHighLight { get; set; }
        public GameObject GetGameObj { get { return gameObject; } }
        public Vector3 GetObjPosition { get { return gameObject.transform.position; } }

        public override SavedNodeBase Save()
        {
            throw new System.NotImplementedException();
        }
        public override void InitializeLoad(SavedNodeBase savedata)
        {
            base.InitializeLoad(savedata);
            SavedMovingNode savedMovingNode = savedata as SavedMovingNode;
            Vector3 SavedPosition = new
                (
                savedMovingNode.Position[0],
                savedMovingNode.Position[1],
                savedMovingNode.Position[2]);
            gameObject.transform.position = SavedPosition;
            NodeD = savedMovingNode.Nd;
        }

        public override void Load()
        {
            base.Load();
        }

        public override void InitializeNode(NodeDesign nd)
        {
            base.InitializeNode(nd);
            lineHandler.InitializeLineHandler(this, attachTrigger.transform, LineTrigger.transform);
        }

        public void MoveObj(Vector3 newPos, Vector3 Offset, bool UseGrid)
        {
            Vector2 GridSize = BTSettings.NodeGridSize;
            Vector3 activeNodePos = UseGrid ? MoveObjectsManager.Instance.MousePositionToGrid(newPos, GridSize, Offset, Vector2.zero) : newPos;
            RaycastHit2D hit = Physics2D.Raycast(activeNodePos, -Vector2.zero);
            if (!hit)
            {
                gameObject.transform.position = activeNodePos;
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
            }
            lineHandler.MoveObj(newPos, Offset, UseGrid);
        }
        public override void AttachLine(LinePoint Line)
        {
            base.AttachLine(Line);
            lineHandler.MoveObj(gameObject.transform.position, Vector3.zero, false);
        }
        public void SetMoveNode()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveObjectsManager.Instance.AddMovableObj(this);
                MoveObjectsManager.Instance.StartMoving();
            }
        }
        public virtual void StartMoveObj()
        {
            SoundManager.Instance.PlayWetPop();
        }

        public virtual void StopMoveObj()
        {

        }
    }
}
