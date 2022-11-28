using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace BehaviorTreePlanner.MenuUi
{
    public class CreateNodeColorPicker : MonoBehaviour
    {
        [SerializeField] private Texture2D reffSprite;
        [SerializeField] private RectTransform LeftBotCornReff;
        [SerializeField] private GameObject colorCursorBorder;
        [SerializeField] private GameObject colorCursor;
        [SerializeField] private GameObject topColor;
        [SerializeField] private GameObject botColor;

        private Camera cam;
        private bool isOpened = false;
        private bool isTopNode = false;
        private bool isSettingColor = false;
        void Awake()
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        void Start()
        {

        }

        void Update()
        {
            SetColor();
        }
        /// <summary>
        /// gets the mouse position divided by the texture
        /// corner position multiplied 1.5 becuase its size in pixels is smaller then the original
        /// </summary>
        private void SetColor()
        {
            if (isSettingColor)
            {
                colorCursorBorder.SetActive(true);

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 0;
                Vector3 mouseToWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseToWorldPos.z = 0;
                colorCursorBorder.transform.position = mouseToWorldPos;
                Vector3 LeftBotCorn = cam.WorldToScreenPoint(LeftBotCornReff.position);
                LeftBotCorn.z = 0;
                float colorPickerCoordsCorrector = 1.5f;
                int xPix = (int)((mousePos.x - LeftBotCorn.x) * colorPickerCoordsCorrector);
                int yPix = (int)((mousePos.y - LeftBotCorn.y) * colorPickerCoordsCorrector);
                colorCursor.GetComponent<Image>().color = reffSprite.GetPixel(xPix, yPix);
                if (isTopNode)
                {
                    topColor.GetComponent<Image>().color = reffSprite.GetPixel(xPix, yPix);
                }
                else
                {
                    botColor.GetComponent<Image>().color = reffSprite.GetPixel(xPix, yPix);
                }
                isSettingColor = Input.GetMouseButton(0);
                if (!isSettingColor)
                {
                    colorCursorBorder.SetActive(false);
                }
            }
        }


        public void StartSetColor()
        {
            if (Input.GetMouseButton(0))
            {
                isSettingColor = true;
            }
        }
        public void ToggleColorPicker(bool istopnode)
        {

            if (isOpened)
            {
                isOpened = false;
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                isOpened = true;
                isTopNode = istopnode;
            }
        }
    }
}