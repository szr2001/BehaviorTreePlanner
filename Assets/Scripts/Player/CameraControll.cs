using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Player
{
    public class CameraControll : MonoBehaviour
    {
        private Vector3 mousePos;
        private Vector3 newMousePos;
        private Vector3 MouseOffset;

        private readonly float MaxZoom = 8;
        private readonly float MinZoom = 3;
        private readonly float MaxCameraDistance = 10;
        private readonly float MovementSpeedDivider = 60;
        void Update()
        {
            MoveCamera();
            Zoom();
            MoveSelection();
        }
        private void MoveCamera()
        {
            if (Input.GetMouseButtonDown(1))
            {
                mousePos = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))
            {
                newMousePos = Input.mousePosition;
                MouseOffset = gameObject.transform.position + ((mousePos - newMousePos) / MovementSpeedDivider);
                float ClampedX = System.Math.Clamp(MouseOffset.x, -MaxCameraDistance, MaxCameraDistance);
                float ClampedY = System.Math.Clamp(MouseOffset.y, -MaxCameraDistance, MaxCameraDistance);
                gameObject.transform.position = new Vector3(ClampedX, ClampedY, MouseOffset.z);
                mousePos = Input.mousePosition;
            }
        }
        private void Zoom()
        {
            float NewZoom = SavedReff.PlayerCamera.orthographicSize += -(Input.GetAxis("Mouse ScrollWheel") * 3);
            SavedReff.PlayerCamera.orthographicSize = Mathf.Clamp((float)System.Math.Round(NewZoom, 1), MinZoom, MaxZoom);
        }
        private void MoveSelection()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(SavedReff.PlayerCamera.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero, 0.1f);
                if (!hit)
                {
                    SavedReff.MoveSelection.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                    SavedReff.MoveSelection.SetActive(true);
                    mousePos = Input.mousePosition;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                ResizeMoveSelection();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Bounds MoveArea = new Bounds(SavedReff.MoveSelection.GetComponent<RectTransform>().anchoredPosition, SavedReff.MoveSelection.GetComponent<RectTransform>().sizeDelta);
                SavedReff.MoveSelection.GetComponent<MoveSelection>().CheckOverlap(MoveArea);
                SavedReff.MoveSelection.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                SavedReff.MoveSelection.SetActive(false);
            }
        }
        private void ResizeMoveSelection() 
        {
            float width = Input.mousePosition.x - mousePos.x;
            float height = Input.mousePosition.y - mousePos.y;
            SavedReff.MoveSelection.GetComponent<RectTransform>().anchoredPosition = new Vector2(mousePos.x, mousePos.y) + new Vector2(width / 2f, height / 2f);
            SavedReff.MoveSelection.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(width),Mathf.Abs(height));
        }
    }
}