using BehaviorTreePlanner.Global;
using System;
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
        private LinePoint ParentLine;
        private List<LinePoint> SpawnedPoints = new();
        private bool IsRoot = false;
        public Vector3 GetObjPosition { get { return Highlight.transform.position;}}
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
            UpdateLineRenderer();
            foreach (LinePoint point in SpawnedPoints) // probl
            {
                point.UpdateLineRenderer();
            }
            if(ParentLine != null)
            {
                ParentLine.UpdateLineRenderer();
            }
        }
        public void InitializeLine(LinePoint caller,bool root)
        {
            ParentLine = caller;
            IsRoot = root;
            if (IsRoot)
            {
                Destroy(LineR);
            }
            UpdateLineRenderer();
        }
        private void UpdateLineRenderer() // probl poate nu se cheama la move ptr ca doar 1 se move
        {
            Debug.Log($"UpdateLIneR; parent null: {ParentLine == null} SpawnedLineCount: {SpawnedPoints.Count}",gameObject);
            if(LineR == null)
            {
                return;
            }
            if (ParentLine != null)
            { 
                LineR.SetPosition(0, new Vector3(ParentLine.GetObjPosition.x, ParentLine.GetObjPosition.y,0));
            }
            LineR.SetPosition(1, new Vector3(GetObjPosition.x, GetObjPosition.y, 0));
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
            SpawnedPoints.Add(SavedReff.SpawnManager.SpawnLinePoint(this,true, false));
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
