using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner
{
    public class LineHandler : IMovable,IAtachLine
    {
        private EditorManager EditorManager;
        public IMovable Parent { get; set; }
        public Transform SpawnedPointPos { get; set; }
        public Transform AttachedPointPos { get; set; }

        public LinePoint SpawnedPoint { get; set; } 
        public LinePoint AttachedPoint { get; set; }

        public void InitializeLineHandler(IMovable parent, Transform attachpointpos,Transform spawnpointpos,EditorManager editormanager)
        {
            EditorManager = editormanager;
            AttachedPointPos = attachpointpos;
            SpawnedPointPos = spawnpointpos;
            Parent = parent;
        }
        public void SpawnLine()
        {
            if (SpawnedPointPos != null && SpawnedPoint == null)
            {
                SpawnedPoint = EditorManager.SpawnManager.SpawnLinePoint(null,false,true);
                SpawnedPoint.transform.position = SpawnedPointPos.position;
                SpawnedPoint.SpawnPoint();
            }
        }
        public GameObject GetGameObj { get { return Parent.GetGameObj; } }
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

        }
        public void StartMoveObj()
        {
        }
        public void AttachLine(LinePoint Line)
        {
            if (AttachedPointPos != null && AttachedPoint == null)
            {
                AttachedPoint = Line;
            }
        }
        public void DestroyLineHandler()
        {
            if(AttachedPoint != null)
            {
                AttachedPoint.DestroyObject();
            }
            if(SpawnedPoint != null)
            {
                SpawnedPoint.DestroyObject();
            }
        }
        public void DeAttachLine()
        {
            AttachedPoint = null;
        }
    }
}
