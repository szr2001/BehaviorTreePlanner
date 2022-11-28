using UnityEngine;

namespace BehaviorTreePlanner.Player
{
    public class CameraControll : MonoBehaviour
    {
        private Vector3 mousePos;
        private Vector3 newMousePos;
        private Vector3 newCameranPos;

        void Update()
        {
            MoveCamera();
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
    }
}