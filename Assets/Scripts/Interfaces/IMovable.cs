using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public interface IMovable
    {
        public void MoveObj(Vector3 newPos);
    }
}
