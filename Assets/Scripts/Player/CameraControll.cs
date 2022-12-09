using BehaviorTreePlanner.Global;
using UnityEngine;

namespace BehaviorTreePlanner.Player
{
    public class CameraControll : MonoBehaviour
    {
        private Vector3 mousePos;
        private Vector3 newMousePos;
        private Vector3 newCameranPos;

        private readonly float MaxZoom = 8;
        private readonly float MinZoom = 3;
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
                newCameranPos = mousePos - newMousePos;
                gameObject.transform.position += newCameranPos / 60;
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