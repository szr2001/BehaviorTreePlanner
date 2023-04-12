using BehaviorTreePlanner.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner.Lines
{
    public class LinePoint : MonoBehaviour,IMovable,IBeginDragHandler,IEndDragHandler,IDragHandler,IObjDestroyable
    {
        public IAtachLine AtachedToObj { get; set; }

        [SerializeField] private GameObject Highlight;
        [SerializeField] private LineRenderer LineR;
        private LinePoint ParentLine;
        private readonly List<LinePoint> SpawnedPoints = new();
        private bool IsRoot = false;
        //on destroy remove itself from parent
        public GameObject GetGameObj { get { return gameObject; } }
        public Vector3 GetObjPosition { get { return Highlight.transform.position;}}
        public void MoveObj(Vector3 newPos, Vector3 Offset, bool UseGrid)
        {
            Vector2 GridSize = SavedSettings.LineGridSize;
            Vector2 CorectionOffset = new(0.08f, 0);
            Vector3 activeLinePos = UseGrid ? SavedReff.MousePositionToGrid(newPos, GridSize, Offset, CorectionOffset) : newPos;
            RaycastHit2D hit = Physics2D.Raycast(activeLinePos, -Vector2.zero);
            if (!hit)
            {
                gameObject.transform.position = activeLinePos;
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
            }
            UpdateLineRenderer();
            foreach (LinePoint point in SpawnedPoints)
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
        private void CheckAttach()
        {
            if(SavedReff.MoveObjectsManager.MoveObjCount > 1)
            {
                return;
            }

            Vector3 activeNodePos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(activeNodePos, -Vector2.zero);
            if (hit && hit.collider.gameObject.TryGetComponent<IAtachLine>(out IAtachLine Itach))
            {
                if (SpawnedPoints.Count > 0)
                {
                    foreach (LinePoint point in SpawnedPoints)
                    {
                        Debug.Log("Check attach Call DestroyPoint", gameObject);
                        point.DestroyObject();
                    }
                    SpawnedPoints.Clear();
                }

                AtachedToObj?.DeAttachLine();
                AtachedToObj = Itach;
                SavedReff.RemoveActiveLine(this.gameObject);
                AtachedToObj.AttachLine(this);
            }
            else
            {
                if (AtachedToObj != null)
                {
                    AtachedToObj.DeAttachLine();
                    AtachedToObj = null;
                    SavedReff.AddActiveLine(this.gameObject);
                }
            }
        }
        public void RemoveLine(LinePoint point)
        {
            SpawnedPoints.Remove(point);
        }
        private void UpdateLineRenderer()
        {
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
            if (AtachedToObj != null)
            {
                SavedReff.MoveObjectsManager.AddMovableObj(this);
                SavedReff.MoveObjectsManager.StartMoving();
            }
            else
            {
                SpawnedPoints.Add(SavedReff.SpawnManager.SpawnLinePoint(this,true, false));
            }
        }
        public void StartMoveObj()
        {
            Highlight.SetActive(true);
        }
        public void StopMoveObj()
        {
            Highlight.SetActive(false);
            CheckAttach();
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

        #region DragHandlers
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
        #endregion
        public void DestroyObject()
        {
            for (int i = 0; i < SpawnedPoints.Count; i++)
            {
                SpawnedPoints[i].DestroyObject();
            }
            if (ParentLine != null && ParentLine.IsRoot)
            {
                ParentLine.RemoveLine(this);
            }
            SavedReff.RemoveActiveLine(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
