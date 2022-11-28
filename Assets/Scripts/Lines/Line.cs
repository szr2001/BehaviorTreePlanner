using UnityEngine;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.SaveGame;
using BehaviorTreePlanner.Interfaces;
using UnityEngine.UI;
using System.Collections;

namespace BehaviorTreePlanner.Lines
{
    public class Line : MonoBehaviour
    {
        public LineDraggerClass LineDraggerC { get; set; }
        public bool IsFixed { get; set; } = false;
        public bool IsMoving { get; set; } = true;
        public bool IsDragging { get; set; } = false;
        public GameObject point1 { get { return _point1; } set { _point1 = value; } }
        public GameObject point2 { get { return _point2; } set { _point2 = value; } }
        [HideInInspector] public int Index { get; set; }
        [SerializeField] private GameObject _point1;
        [SerializeField] private GameObject _point2;
        private Image _point2Image;
        private void Awake()
        {
            LineDraggerC = new LineDraggerClass(gameObject, point2);
            _point2Image = _point2.GetComponent<Image>();
            _point2Image.raycastTarget = false;
        }
        void Update()
        {
            UpdateLineRenderers();
            MoveLine();
            CheckClicked();
        }
        /// <summary>
        /// Check when you click when the line is attached to the mouse 
        /// </summary>
        private void CheckClicked()
        {
            if (!IsFixed)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //checks if the collidor has needed interface
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero, 0.1f); // incearca fara vector2
                    if (hit.collider != null)
                    {
                        IAttachLine ial = hit.collider.gameObject.GetComponent<IAttachLine>();
                        if (ial != null)
                        {
                            ial.IAttachLine(gameObject); //add bool to IattachLine
                            IsFixed = true;
                            IsMoving = false;
                        }
                    }
                    //create a new line  if it dosent
                    else
                    {
                        StartLine();
                    }
                    _point2Image.raycastTarget = true;
                }
            }
        }
        /// <summary>
        /// Moves the line to the mouse if the bool is true
        /// </summary>
        private void MoveLine()
        {
            if (IsMoving)
            {
                LineDraggerC.SetLinesLocation();
                if (SavedSettings.EnableSnapToGrid)
                {
                    Vector2 mospos = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
                    Vector3 offset = new Vector3(-0.08f, 0.2f, 0);
                    Vector3 activeNodePos = new Vector3(mospos.x, mospos.y, 0);
                    activeNodePos.x = Mathf.Round(activeNodePos.x);
                    activeNodePos.y = Mathf.Round(activeNodePos.y);
                    if (activeNodePos.x % 1 == 0 && activeNodePos.y % 1 == 0)
                    {
                        ChangePoint2(activeNodePos + offset);
                    }
                }
                else
                {
                    Vector2 mospos = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
                    ChangePoint2(mospos);
                }
                // if left click mouse, call the distroy method.
                if (Input.GetMouseButtonDown(1))
                {
                    DestroyLine();
                }
            }
        }
        /// <summary>
        /// updates the line renderer to always point to point1 and point2
        /// </summary>
        private void UpdateLineRenderers()
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(_point1.transform.position.x, _point1.transform.position.y, 0));
            gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(_point2.transform.position.x, _point2.transform.position.y, 0));
        }
        /// <summary>
        /// Spawns a new line
        /// </summary>
        private void StartLine()
        {
            LineDraggerC.StartLine();
            IsFixed = true;
            IsMoving = false;
        }
        public void LoadLine(LineSaveInfo lineInfo)
        {

        }
        public void StartDrag()
        {
            SetIsMoving(true);
            IsDragging = true;
        }
        public void StopDrag()
        {
            SetIsMoving(false);
            StartCoroutine(DelayStopDrag());
        }
        //needed a delay so the events on click dont run when dragging
        private IEnumerator DelayStopDrag()
        {
            yield return new WaitForSeconds(0.3f);
            IsDragging = false;
        }
        public void SetIsMoving(bool ismoving)
        {
            IsMoving = ismoving;
        }
        public void StartLineOnLeftClick()
        {
            if (!IsDragging)
            {
                StartLine();
            }
        }
        public void DestroyLine()
        {
            LineDraggerC.DeleteLines();
            Destroy(gameObject);
        }
        public void ChangePoint1(Vector2 newLoc)
        {
            _point1.transform.position = new Vector3(newLoc.x, newLoc.y, 0);
        }
        public void ChangePoint2(Vector2 newLoc)
        {
            _point2.transform.position = new Vector3(newLoc.x, newLoc.y, 0);
        }
    }
}