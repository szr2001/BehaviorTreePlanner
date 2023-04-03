using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public interface IMovable
    {
        Vector3 GetStartPosition { get;}
        public void MoveObj(Vector3 MousePos,Vector3 Offset);
        public void Select();
        public void Deselect();
    }
}
