using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public interface IMovable
    {
        Vector3 GetObjPosition { get;}
        public void MoveObj(Vector3 NewPos, Vector3 Offset,bool UseGrid);
        public void StartMoveObj();
        public void StopMoveObj();
    }
}
