using UnityEngine;
using BehaviorTreePlanner.Lines;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace BehaviorTreePlanner.Global
{
    public class SelectionBox : MonoBehaviour
    {
        [SerializeField] private Image MainWindow;
        [SerializeField] private Image MainWindowArrow;
        [SerializeField] private RectTransform RectTrans;

        private bool IsResizingArea = false;
        private Vector2 MosPoss;
        void Update()
        {
            SelectObjects();
        }

        private void SelectObjects()
        {
            //check if is spawning lines/nodes or is mouse over ui, if yes, return
            if (SavedReff.IsOverUi)
            {
                // Handle selection box reset if mouse goes over ui and code stops at return
                if (IsResizingArea)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        Bounds MoveArea = new(RectTrans.anchoredPosition, RectTrans.sizeDelta); //force create an area using rect trans
                        ResetSelectionBox(); // reset selection box info
                        IsResizingArea = false; //set resizing area to false 
                    }
                }
                return;
            }

            //initialize begin drag on mouse button down
            if (Input.GetMouseButtonDown(0))
            {
                ResetSelectionBox(); //reset previous info
                Debug.Log("InputHandler check if hit");
                //check if it clicked on an empty space to begin selecting area
                RaycastHit2D hit = Physics2D.Raycast(SavedReff.PlayerCamera.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero, 0.1f);
                if (!hit)
                {
                    SavedReff.MoveObjectsManager.ClearMovableObj();
                    IsResizingArea = true; //set is resizing to true to handle when the mouse goes over ui and to be able to reset
                                           //set mouse pozition to the pozition where selection begine to be used in resizing the area math
                    MosPoss = Input.mousePosition;
                }

            }
            //if the mouse is still down, resize the selected area
            if (Input.GetMouseButton(0) && IsResizingArea)
            {
                Debug.Log("InputHandler Resize");
                ResizeSelectedArea();
            }
            //if mouse button up, stop resizing area and call method to check what objects where inside selected area
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("InputHandler released buton check overlap");
                IsResizingArea = false; // set resizing area to false because the mouse button is not pressed anymore
                Bounds MoveArea = new(RectTrans.anchoredPosition, RectTrans.sizeDelta); //create the area that was selected using the rect transform
                GetOverlapedObjects(MoveArea); //call method to find objects inside provided area
                                               //MAKE THIS 3 LINES A METHOD INSTEAD
            }
        }
        
        private void GetOverlapedObjects(Bounds Area)
        {
            RectTrans.sizeDelta = Vector2.zero;
            foreach(GameObject Node in SavedReff.ActiveNodes)
            {
                if (IsPointInsideArea(SavedReff.PlayerCamera.WorldToScreenPoint(Node.transform.position), Area))
                {
                    SavedReff.MoveObjectsManager.AddMovableObj(Node.GetComponent<IMovable>());
                }
            }
            foreach (GameObject Line in SavedReff.ActiveLines)
            {
                if (IsPointInsideArea(SavedReff.PlayerCamera.WorldToScreenPoint(Line.transform.position), Area))
                {
                    SavedReff.MoveObjectsManager.AddMovableObj(Line.GetComponent<IMovable>());
                }
            }
        }

        private void ResizeSelectedArea()
        {
            float width = Input.mousePosition.x - MosPoss.x;
            float height = Input.mousePosition.y - MosPoss.y;
            RectTrans.anchoredPosition = new Vector2(MosPoss.x, MosPoss.y) + new Vector2(width / 2f, height / 2f);
            RectTrans.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        }
        private void ResetSelectionBox()
        {
            RectTrans.sizeDelta = Vector2.zero;
        }
        
        private bool IsPointInsideArea(Vector2 poss, Bounds area)
        {
            return poss.x > area.min.x && poss.x < area.max.x
                && poss.y > area.min.y && poss.y < area.max.y;
        }

        //public void CheckOverlap(Bounds Area)
        //{
        //    ClearOverlapedActors();
        //    foreach(GameObject N in SavedReff.ActiveNodes)
        //    {
        //        if (IsPointInsideArea(SavedReff.PlayerCamera.WorldToScreenPoint(N.transform.position), Area))
        //        {
        //            OverlapedObjects.Add(N);
        //        }
        //    }
        //    foreach(GameObject L in SavedReff.ActiveLines)
        //    {
        //        LinePoint ActiveLine = L.GetComponent<Line>().Point2;
        //        if (IsPointInsideArea(SavedReff.PlayerCamera.WorldToScreenPoint(ActiveLine.GetPointLocation()), Area))
        //        {
        //            OverlapedObjects.Add(ActiveLine.gameObject);
        //        }
        //    }
        //    if (OverlapedObjects.Count > 1)
        //    {
        //        TurnONSelectVisualsOnObj();
        //        SetVisibilityMovingSection(false);
        //        SavedReff.IsMovingSelectionActive = true;
        //    }
        //    else
        //    {
        //        SetEnableMovingSection(false);
        //    }
        //}
        //private void CheckInput()
        //{
        //    if (SavedReff.IsMovingSelectionActive)
        //    {
        //        if (Input.GetMouseButtonDown(1))
        //        {
        //            //if mouse right button, cancel
        //            SetEnableMovingSection(false);
        //        }
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            RaycastHit2D hitt = Physics2D.Raycast(SavedReff.PlayerCamera.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero, 0.1f);
        //            if (hitt)
        //            {
        //                if (Input.GetMouseButton(0))
        //                {
        //                    IsMovingObjects = true;
        //                }
        //            }
        //            else
        //            {
        //                SetEnableMovingSection(false);
        //            }
        //        }
        //    }
        //}
        //private void MoveObjects()
        //{
        //    if (IsMovingObjects)
        //    {
        //        GetObjectsMouseOffset();
        //        if (Input.GetMouseButtonUp(0))
        //        {
        //            SetEnableMovingSection(false);
        //        }
        //        if (SavedSettings.EnableSnapToGrid)
        //        {
        //            Vector2 GridSize;
        //            foreach (GameObject G in OverlapedObjects)
        //            {
        //                if(G.TryGetComponent<MovingNode>(out _))
        //                {
        //                    GridSize = SavedSettings.NodeGridSize;
        //                }
        //                else
        //                {
        //                    GridSize = SavedSettings.LineGridSize;
        //                }
        //                Vector3 newObjOffset = (Vector3)OverlapedObjectsMouseOffset[OverlapedObjects.IndexOf(G)];
        //                Vector3 newObjLocation = SavedReff.MousePositionToGrid(MosPoss, GridSize, newObjOffset);
        //                RaycastHit2D hit = Physics2D.Raycast(newObjLocation, -Vector2.zero);
        //                if (!hit)
        //                {
        //                    G.GetComponent<IMovable>().MoveObj(new Vector3(newObjLocation.x, newObjLocation.y, 0));
        //                }
        //            }
        //        }
        //        else //Raycast will hit the node under the mouse and will not move,it looks like its teleporting,disable moved node collider
        //        {
        //            foreach(GameObject G in OverlapedObjects)
        //            {
        //                RaycastHit2D hit = Physics2D.Raycast(MosPoss, -Vector2.zero);
        //                if (!hit) // add ignore self
        //                {
        //                    Vector3 newObjOffset = (Vector3)OverlapedObjectsMouseOffset[OverlapedObjects.IndexOf(G)];
        //                    G.GetComponent<IMovable>().MoveObj(new Vector3(MosPoss.x, MosPoss.y, 0) - newObjOffset);
        //                }
        //            }
        //        }
        //    }
        //}
        //private void ClearOverlapedActors()
        //{
        //    foreach(GameObject G in OverlapedObjects)
        //    {
        //        G.TryGetComponent<ISelectable>(out ISelectable Is);
        //        Is.Deselect();
        //    }
        //    OverlapedObjects.Clear();
        //    OverlapedObjectsMouseOffset.Clear();
        //}
        //private void SetEnableMovingSection(bool isEnabled) // if true remove overlaped actors
        //{
        //    SavedReff.IsMovingSelectionActive = isEnabled;
        //    gameObject.SetActive(isEnabled);
        //    if (isEnabled == false)
        //    {
        //        SetVisibilityMovingSection(true);
        //        TurnOFFSelectVisualsOnObj();
        //        ClearOverlapedActors();
        //        IsMovingObjects = false;
        //    }
        //}
        //private void SetVisibilityMovingSection(bool IsVisibility)
        //{
        //    MainWindowArrow.enabled = IsVisibility;
        //    MainWindow.enabled = IsVisibility;
        //}
        //private bool IsPointInsideArea(Vector2 poss, Bounds area)
        //{
        //    return poss.x > area.min.x && poss.x < area.max.x
        //        && poss.y > area.min.y && poss.y < area.max.y;
        //}
        //private void GetObjectsMouseOffset()
        //{
        //    MosPoss = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
        //    foreach (GameObject G in OverlapedObjects)
        //    {
        //        Vector2 GOffset = MosPoss - (Vector2)G.transform.position;
        //        OverlapedObjectsMouseOffset.Add(GOffset);
        //    }
        //}
        //private void TurnONSelectVisualsOnObj()
        //{
        //    foreach(GameObject G in OverlapedObjects)
        //    {
        //        G.GetComponent<ISelectable>().Select();
        //    }
        //}
        //private void TurnOFFSelectVisualsOnObj()
        //{
        //    foreach (GameObject G in OverlapedObjects)
        //    {
        //        G.GetComponent<ISelectable>().Deselect();
        //    }
        //}
        private enum SelectionBoxState
        {
            Selecting,
            Moving
        }
    }
}
