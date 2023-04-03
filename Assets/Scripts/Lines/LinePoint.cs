using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Lines
{
    public class LinePoint : MonoBehaviour,IMovable
    {
        public GameObject OwnerParent { get; set; }
        [field: SerializeField] private GameObject Highlight;
        [field: SerializeField] private LineRenderer LineR;
        public Vector3 GetStartPosition { get { return gameObject.transform.position;}}
        public void MoveObj(Vector3 newPos, Vector3 Offset)
        {
            Vector2 GridSize = SavedSettings.LineGridSize;
            Vector3 activeLinePos = SavedReff.MousePositionToGrid(newPos, GridSize, Offset);
            RaycastHit2D hit = Physics2D.Raycast(activeLinePos, -Vector2.zero);
            if (!hit)
            {
                gameObject.transform.position = activeLinePos;
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
            }
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
