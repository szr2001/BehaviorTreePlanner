using BehaviorTreePlanner.Global;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner.Lines
{
    public class LinePoint : MonoBehaviour,IMovable,IBeginDragHandler,IEndDragHandler,IDragHandler
    {
        public GameObject OwnerParent { get; set; }
        [SerializeField] private GameObject Highlight;
        [SerializeField] private LineRenderer LineR;
        private List<LinePoint> SpawnedPoints = new();
        private bool IsRoot = false;
        public Vector3 GetStartPosition { get { return gameObject.transform.position;}}
        public void MoveObj(Vector3 newPos, Vector3 Offset, bool UseGrid)
        {
            Vector2 GridSize = SavedSettings.LineGridSize;
            Vector3 activeLinePos = UseGrid ? SavedReff.MousePositionToGrid(newPos, GridSize, Offset) : newPos;
            RaycastHit2D hit = Physics2D.Raycast(activeLinePos, -Vector2.zero);
            if (!hit)
            {
                gameObject.transform.position = activeLinePos;
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
            }
            foreach(LinePoint point in SpawnedPoints)
            {
                point.UpdateLineRenderer();
            }
        }
        private void UpdateLineRenderer()
        {

        }
        public void SpawnPoint()
        {
            if (IsRoot)
            {
                if(SpawnedPoints.Count >= 1)
                {
                    return;
                }
            }
            SpawnedPoints.Add(SavedReff.SpawnManager.SpawnLinePoint(true, false));
        }
        public void SetRoot()
        {
            IsRoot = true;
        }
        public void Select()
        {
            Highlight.SetActive(true);
        }

        public void Deselect()
        {
            Highlight.SetActive(false);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Highlight.activeSelf)
            {
                StartMove();
            }
            else
            {
                SpawnPoint();
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            StopMove();
        }
        public void OnDrag(PointerEventData eventData)
        {
        }
        private void StartMove()
        {
            if (!IsRoot)
            {
                SavedReff.MoveObjectsManager.AddMovableObj(this);
                SavedReff.MoveObjectsManager.StartMoving();
            }
        }
        private void StopMove()
        {
            SavedReff.MoveObjectsManager.StopMoving();
        }
    }
}
