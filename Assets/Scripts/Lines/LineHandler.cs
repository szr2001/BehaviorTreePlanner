using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner
{
    public class LineHandler : IMovable
    {
        public IMovable Parent { get; set; }
        public Transform SpawnedPointPos { get; set; }
        public Transform AttachedPointPos { get; set; }

        private LinePoint SpawnedPoint;
        private LinePoint AttachedPoint;

        public void InitializeLineHandler(IMovable parent, Transform attachpointpos,Transform spawnpointpos)
        {
            AttachedPointPos = attachpointpos;
            SpawnedPointPos = spawnpointpos;
            Parent = parent;
        }
        public void SpawnLine()
        {
            if (SpawnedPointPos != null && SpawnedPoint == null)
            {
                SpawnedPoint = SavedReff.SpawnManager.SpawnLinePoint(null,false,true);
                SpawnedPoint.transform.position = SpawnedPointPos.position;
                SpawnedPoint.SpawnPoint();
            }
        }
        public void AttachLine(LinePoint point)
        {
            if(AttachedPointPos != null && AttachedPoint == null)
            {
                AttachedPoint = point;
            }
        }
        public Vector3 GetObjPosition { get { return Parent.GetObjPosition; } }
        public void MoveObj(Vector3 NewPos, Vector3 Offset, bool UseGrid)
        {
            if(SpawnedPoint != null)
            {
                SpawnedPoint.MoveObj(SpawnedPointPos.position, Offset, false);
            }
            if (AttachedPoint != null)
            {
                AttachedPoint.MoveObj(AttachedPointPos.position, Offset, false);
            }
        }
        public void StopMoveObj()
        {
            throw new System.NotImplementedException();
        }
        public void StartMoveObj()
        {
            throw new System.NotImplementedException();
        }
    }
}
