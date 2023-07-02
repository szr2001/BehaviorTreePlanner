using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Nodes;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner.Lines
{
    public class LinePoint : MonoBehaviour,IMovable,IBeginDragHandler,IEndDragHandler,IDragHandler,IObjDestroyable
    {
        public EditorManager editorManager;
        public NodeBase AtachedToNode { get; set; }

        [SerializeField] private GameObject Highlight;
        [SerializeField] private LineRenderer LineR;
        [SerializeField] private LinePoint ParentLine;
        [SerializeField] private readonly List<LinePoint> SpawnedPoints = new();
        [field: SerializeField]public bool IsRoot { get; set; } = false;
        [field: SerializeField]public bool IsAtachedToNode { get; set; } = false;
        [field: SerializeField] public int SaveIndex { get; set; }
        public GameObject GetGameObj { get { return gameObject; } }
        public Vector3 GetObjPosition { get { return Highlight.transform.position;}}
        private SavedLinePoint saveData;
        public void InitializeSave(int index)
        {
            SaveIndex = index;
        }

        public SavedLinePoint Save()
        {
            float[] linepos = new float[] { gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z };
            float[] lineRendererpos1 = new float[0];
            float[] lineRendererpos2 = new float[0];
            if (LineR != null) 
            {
                Vector3 LinerendererPos1V = LineR.GetPosition(0);
                lineRendererpos1 = new float[]{ LinerendererPos1V.x, LinerendererPos1V.y, LinerendererPos1V.z};

                Vector3 LinerendererPos2V = LineR.GetPosition(1);
                lineRendererpos2 = new float[]{ LinerendererPos2V.x, LinerendererPos2V.y, LinerendererPos2V.z};
            }

            List<int> spawnedLinesIndexes = new();
            foreach(LinePoint point in SpawnedPoints)
            {
                spawnedLinesIndexes.Add(point.SaveIndex);
            }

            return new SavedLinePoint
                (
                    IsRoot ? (byte)1 : (byte)0,
                    SaveIndex,
                    linepos,
                    lineRendererpos1,
                    lineRendererpos2,
                    ParentLine != null ? ParentLine.SaveIndex : -1,
                    spawnedLinesIndexes.ToArray(),
                    AtachedToNode != null ? AtachedToNode.SaveIndex : -1,
                    IsAtachedToNode ? (byte)1 : (byte)0


                );
        }
        public void InitializeLoad(SavedLinePoint savedata)
        {
            saveData = savedata;
            SaveIndex = savedata.LineIndex;
            IsRoot = savedata.IsRoot == (byte)0 ? false : true;
            IsAtachedToNode = savedata.IsAtachedToNode == (byte)0 ? false : true;

            Vector3 savedlinepos = new
                (
                    savedata.Position[0],
                    savedata.Position[1],
                    savedata.Position[2]
                );

            gameObject.transform.position = savedlinepos;

            if (!IsRoot)
            {
                Vector3 savedlinerender1 = new
                    (
                        savedata.LineRendererPos1[0],
                        savedata.LineRendererPos1[1],
                        savedata.LineRendererPos1[2]
                    );

                LineR.SetPosition(0, savedlinerender1);
                Vector3 savedlinerender2 = new
                    (
                        savedata.LineRendererPos2[0],
                        savedata.LineRendererPos2[1],
                        savedata.LineRendererPos2[2]
                    );

                LineR.SetPosition(1, savedlinerender2);
            }
            else
            {
                Destroy(LineR);
            }

        }

        public void Load()
        {
            ParentLine = saveData.ParentLineIndex == -1 ? null : editorManager.SpawnManager.ActiveLines[saveData.ParentLineIndex];
            AtachedToNode = saveData.AtachedToNodeIndex == -1 ? null : editorManager.SpawnManager.ActiveNodes[saveData.AtachedToNodeIndex];
            foreach(int spawnedlineindex in saveData.SpawnedLinesIndex)
            {
                if(spawnedlineindex == -1)
                {
                    return;
                }
                SpawnedPoints.Add(editorManager.SpawnManager.ActiveLines[spawnedlineindex]);
            }
        }
        public void MoveObj(Vector3 newPos, Vector3 Offset, bool UseGrid)
        {
            Vector2 GridSize = SavedSettings.LineGridSize;
            Vector2 CorectionOffset = new(0.08f, 0);
            Vector3 activeLinePos = UseGrid ? editorManager.MoveObjectsManager.MousePositionToGrid(newPos, GridSize, Offset, CorectionOffset) : newPos;
            
            if(IsRoot || IsAtachedToNode)
            {
                gameObject.transform.position = activeLinePos;
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(activeLinePos, -Vector2.zero);
                if (!hit)
                {
                    gameObject.transform.position = activeLinePos;
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
                }
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
        public void InitializeLine(LinePoint caller,bool root,EditorManager editorManager)
        {
            this.editorManager = editorManager;
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
            if(editorManager.MoveObjectsManager.MoveObjCount > 1)
            {
                return;
            }

            Vector3 activeNodePos = editorManager.PlayerControll.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(activeNodePos, -Vector2.zero);
            if (hit && hit.collider.gameObject.TryGetComponent<NodeBase>(out NodeBase attachtoNode))
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
                if(AtachedToNode != null)
                {
                    AtachedToNode.DeAttachLine();
                }
                AtachedToNode = attachtoNode;
                AtachedToNode.AttachLine(this);
            }
            else
            {
                if (AtachedToNode != null)
                {
                    AtachedToNode.DeAttachLine();
                    AtachedToNode = null;
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
            if (AtachedToNode != null)
            {
                editorManager.MoveObjectsManager.AddMovableObj(this);
                editorManager.MoveObjectsManager.StartMoving();
            }
            else
            {
                LinePoint spawnedpoint = editorManager.SpawnManager.SpawnLinePoint(this, true, false);
                spawnedpoint.transform.localPosition = transform.localPosition - new Vector3(0, 10, 0);
                SpawnedPoints.Add(spawnedpoint);
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
                editorManager.MoveObjectsManager.AddMovableObj(this);
                editorManager.MoveObjectsManager.StartMoving();
            }
        }
        private void StopMove()
        {
            editorManager.MoveObjectsManager.StopMoving();
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
            Debug.Log("Destroyed line");
            try 
            {
                int maxloop = SpawnedPoints.Count;
                for (int i = 0; i < maxloop; i++)
                {
                    if(SpawnedPoints[0] == null)
                    {
                        continue;
                    }

                    SpawnedPoints[0].DestroyObject();
                }
                if(ParentLine != null)
                {
                    ParentLine.RemoveLine(this);
                    if (ParentLine.IsRoot)
                    {
                        editorManager.SpawnManager.RemoveActiveLine(ParentLine);
                        Destroy(ParentLine.gameObject);
                    }
                }
                editorManager.SpawnManager.RemoveActiveLine(this);
                Destroy(gameObject);
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
