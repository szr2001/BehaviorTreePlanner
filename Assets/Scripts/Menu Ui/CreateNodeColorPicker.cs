using BehaviorTreePlanner.Global;
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

        private bool isOpened = false;
        private bool isTopNode = false;
        private bool isSettingColor = false;
        private Image colorCursorImg;
        private Image topColorImg;
        private Image botColorImg;
        void Awake()
        {
            colorCursorImg = colorCursor.GetComponent<Image>();
            topColorImg = topColor.GetComponent<Image>();
            botColorImg = botColor.GetComponent<Image>();
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
                Vector3 mouseToWorldPos = SavedReff.PlayerCamera.ScreenToWorldPoint(Input.mousePosition);
                mouseToWorldPos.z = 0;
                colorCursorBorder.transform.position = mouseToWorldPos;
                Vector3 LeftBotCorn = SavedReff.PlayerCamera.WorldToScreenPoint(LeftBotCornReff.position);
                LeftBotCorn.z = 0;
                float colorPickerCoordsCorrector = 1.5f;
                int xPix = (int)((mousePos.x - LeftBotCorn.x) * colorPickerCoordsCorrector);
                int yPix = (int)((mousePos.y - LeftBotCorn.y) * colorPickerCoordsCorrector);
                colorCursorImg.color = reffSprite.GetPixel(xPix, yPix);
                if (isTopNode)
                {
                    topColorImg.color = reffSprite.GetPixel(xPix, yPix);
                }
                else
                {
                    botColorImg.color = reffSprite.GetPixel(xPix, yPix);
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
        public void DisableColorPicker()
        {
            isOpened = false;
            gameObject.SetActive(false);
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