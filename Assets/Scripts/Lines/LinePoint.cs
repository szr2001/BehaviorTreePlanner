using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Lines
{
    public class LinePoint : MonoBehaviour,IMovable
    {
        public void MoveObj(Vector3 newPos)
        {
            gameObject.transform.position = newPos;
        }
        public Vector3 GetPointLocation()
        {
            return gameObject.transform.position;
        }
    }
}
