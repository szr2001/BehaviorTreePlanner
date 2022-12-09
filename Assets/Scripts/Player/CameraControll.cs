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
    }
}