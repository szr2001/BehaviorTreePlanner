using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public interface IMovable
    {
        public void MoveObj(Vector3 MousePos);
        public void Select();
        public void Deselect();
    }
}
