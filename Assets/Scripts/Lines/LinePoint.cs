using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Lines
{
    public class LinePoint : MonoBehaviour,IMovable
    {
        [field: SerializeField] private GameObject Highlight;
        public void MoveObj(Vector3 newPos)
        {
            gameObject.transform.position = newPos;
        }
        public Vector3 GetPointLocation()
        {
            return gameObject.transform.position;
        }

        public void Select()
        {
            Highlight.SetActive(true);
        }

        public void Deselect()
        {
            Highlight.SetActive(false);
        }
    }
}
