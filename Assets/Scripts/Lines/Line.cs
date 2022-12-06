using UnityEngine;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.SaveGame;
using BehaviorTreePlanner.Interfaces;
using UnityEngine.UI;
using System.Collections;
using BehaviorTreePlanner.MenuUi;

namespace BehaviorTreePlanner.Lines
{
    public class Line : MonoBehaviour
    {
        public NodeBase AttachedTo { get; set; }
        public LineDraggerClass LineDraggerC { get; set; }
        public bool IsFixed { get; set; } = false;
        public bool IsMoving { get; set; } = true;
        public bool IsDragging { get; set; } = false;
        public GameObject Point1 { get { return _point1; } set { _point1 = value; } }
        public GameObject Point2 { get { return _point2; } set { _point2 = value; } }
        [HideInInspector] public int Index { get; set; }
        [SerializeField] private GameObject _point1;
        [SerializeField] private GameObject _point2;

        private Image _point2Image;
        private LineRenderer lineRenderer;
        private void Start()
        {
            ChangePoint2(Point1.transform.position);
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }
        private void Awake()
        {
            LineDraggerC = new LineDraggerClass(gameObject, Point2);
            _point2Image = _point2.GetComponent<Image>();
            _point2Image.raycastTarget = false;
        }
        void Update()
        {
            UpdateLineRenderers();
            CheckClicked();
            MoveLine();
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
                    RaycastHit2D hit = Physics2D.Raycast(SavedReff.PlayerCamera.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero, 0.1f);
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.TryGetComponent<IAttachLine>(out var ial))
                        {
                            ial.IAttachLine(gameObject.GetComponent<Line>());
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
                    Vector2 mospos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                    Vector3 offset = new Vector3(-0.08f, 0.51f, 0);
                    Vector3 activeNodePos = new Vector3(mospos.x, mospos.y, 0);
                    activeNodePos.x = Mathf.Round(activeNodePos.x);
                    activeNodePos.y = Mathf.Round(activeNodePos.y);
                    if (activeNodePos.x % 1 == 0 && activeNodePos.y % 1 == 0)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(activeNodePos + offset, -Vector2.zero);
                        if (hit)
                        {
                            if (hit.collider.gameObject.TryGetComponent<IAttachLine>(out var attacL))
                            {
                                ChangePoint2(activeNodePos + offset);
                            }
                        }
                        else
                        {
                            ChangePoint2(activeNodePos + offset);
                        }
                    }
                }
                else
                {
                    Vector2 mospos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(mospos, -Vector2.zero);
                    string NodeTag = "Node";
                    if (!hit)
                    {
                        ChangePoint2(mospos);
                    }
                    else if (hit.collider.gameObject.CompareTag(NodeTag))
                    {
                        ChangePoint2(mospos);
                    }
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
            lineRenderer.SetPosition(0, new Vector3(_point1.transform.position.x, _point1.transform.position.y, 0));
            lineRenderer.SetPosition(1, new Vector3(_point2.transform.position.x, _point2.transform.position.y, 0));
        }
        /// <summary>
        /// Spawns a new line
        /// </summary>
        private void StartLine()
        {
            IsFixed = true;
            IsMoving = false;
            LineDraggerC.StartLine();
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
            StartCoroutine(DelaySetIsDraggingFalse());
            if (AttachedTo != null)
            {
                AttachedTo.LineAttacherC.DeatachLine();
            }
            RaycastHit2D hit = Physics2D.Raycast(SavedReff.PlayerCamera.ScreenToWorldPoint(Input.mousePosition), -Vector2.zero, 0.1f);
            string NodeButtonTag = "NodeButton";
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent<IAttachLine>(out var ial))
                {
                    LineDraggerC.DeleteLines();
                    ial.IAttachLine(gameObject.GetComponent<Line>());
                    if (hit.collider.gameObject.CompareTag(NodeButtonTag))
                    {
                        hit.collider.gameObject.GetComponent<NodeButton>().SetSpawnNode();
                    }
                }
            }
        }
        //needed a delay so the event on click dosent run when on end drag
        private IEnumerator DelaySetIsDraggingFalse()
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