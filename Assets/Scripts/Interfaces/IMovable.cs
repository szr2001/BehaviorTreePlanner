using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public interface IMovable
    {
        Vector3 GetObjPosition { get;}
        void MoveObj(Vector3 NewPos, Vector3 Offset,bool UseGrid);
        void StartMoveObj();
        void StopMoveObj();
    }
}
