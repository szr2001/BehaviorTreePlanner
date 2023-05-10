using UnityEngine;
using BehaviorTreePlanner.Lines;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace BehaviorTreePlanner.Global
{
    public class SelectionBox : MonoBehaviour
    {
        [SerializeField] private EditorManager EditorManager;
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
            if (EditorManager.IsOverUi)
            {
                // Handle selection box reset if mouse goes over ui and code stops at return
                if (IsResizingArea)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
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
                //check if it clicked on an empty space to begin selecting area
                RaycastHit2D hit = Physics2D.Raycast(EditorManager.PlayerControll.PlayerCamera.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero, 0.1f);
                if (!hit)
                {
                    EditorManager.MoveObjectsManager.ClearMovableObj();
                    IsResizingArea = true; //set is resizing to true to handle when the mouse goes over ui and to be able to reset
                                           //set mouse pozition to the pozition where selection begine to be used in resizing the area math
                    MosPoss = Input.mousePosition;
                }

            }
            //if the mouse is still down, resize the selected area
            if (Input.GetMouseButton(0) && IsResizingArea)
            {
                ResizeSelectedArea();
            }
            //if mouse button up, stop resizing area and call method to check what objects where inside selected area
            if (Input.GetMouseButtonUp(0))
            {
                IsResizingArea = false; // set resizing area to false because the mouse button is not pressed anymore
                Bounds MoveArea = new(RectTrans.anchoredPosition, RectTrans.sizeDelta); //create the area that was selected using the rect transform
                GetOverlapedObjects(MoveArea); //call method to find objects inside provided area
                                               //MAKE THIS 3 LINES A METHOD INSTEAD
            }
        }
        private void GetOverlapedObjects(Bounds Area)
        {
            RectTrans.sizeDelta = Vector2.zero;
            foreach(GameObject Node in EditorManager.SpawnManager.ActiveNodes)
            {
                if (IsPointInsideArea(EditorManager.PlayerControll.PlayerCamera.WorldToScreenPoint(Node.transform.position), Area))
                {
                    EditorManager.MoveObjectsManager.AddMovableObj(Node.GetComponent<IMovable>());
                }
            }
            foreach (GameObject Line in EditorManager.SpawnManager.ActiveLines)
            {
                if (IsPointInsideArea(EditorManager.PlayerControll.PlayerCamera.WorldToScreenPoint(Line.transform.position), Area))
                {
                    EditorManager.MoveObjectsManager.AddMovableObj(Line.GetComponent<IMovable>());
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
    }
}
