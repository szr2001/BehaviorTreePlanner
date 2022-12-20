using UnityEngine;
using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.SaveGame;
using BehaviorTreePlanner.Interfaces;
using BehaviorTreePlanner.Nodes;
using UnityEngine.UI;
using System.Collections;
using BehaviorTreePlanner.MenuUi;
using Unity.VisualScripting;

namespace BehaviorTreePlanner.Lines
{
    public class Line : MonoBehaviour
    {
        public NodeBase NodeRoot { get; set; }
        public Color LineColor { get; set; }
        public MovingNode AttachedToNodeReff { get; set; }
        public Line AttachedToLineReff { get; set; }
        public LineDraggerClass LineDraggerC { get; set; }
        public bool IsFixed { get; set; } = false;
        public bool IsMoving { get; set; } = true;
        public bool IsDragging { get; set; } = false;
        [HideInInspector] public int Index { get; set; }
        [field: SerializeField] public LinePoint Point1 { get; set; }
        [field: SerializeField] public LinePoint Point2 { get; set; }

        private Image Point2Image;
        private LineRenderer lineRenderer;

        private void Start()
        {
            ChangePoint2(Point1.GetPointLocation());
        }
        private void Awake()
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            LineDraggerC = new LineDraggerClass(gameObject, Point2.gameObject);
            Point2Image = Point2.GetComponent<Image>();
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
                            SavedReff.IsSpawningLines = false;
                            IsFixed = true;
                            IsMoving = false;
                        }
                    }
                    //create a new line  if it dosent
                    else
                    {
                        RaycastHit2D Phit = Physics2D.Raycast(Point2.GetPointLocation(), -Vector2.zero, 0.1f);
                        if (!Phit)
                        {
                            StartLine();
                        }
                    }
                    Point2Image.raycastTarget = true;
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
                Vector2 mospos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                if (SavedSettings.EnableSnapToGrid)
                {
                    float GridSize = 0.5f;
                    Vector3 offset = new Vector3(-0.08f, 0, 0);
                    Vector3 activeNodePos = new Vector3(mospos.x, mospos.y, 0);
                    activeNodePos.x = Mathf.Round(activeNodePos.x / GridSize) * GridSize;
                    activeNodePos.y = Mathf.Round(activeNodePos.y / GridSize) * GridSize;
                    RaycastHit2D hit = Physics2D.Raycast(activeNodePos + offset, -Vector2.zero);
                    if (hit)
                    {
                        if (hit.collider.gameObject.TryGetComponent<IAttachLine>(out _))
                        {
                            ChangePoint2(activeNodePos + offset);
                        }
                    }
                    else
                    {
                        ChangePoint2(activeNodePos + offset);
                    }
                }
                else
                {
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
                    SavedReff.IsSpawningLines = false;
                    DestroyLine();
                }
            }
        }
        /// <summary>
        /// updates the line renderer to always point to point1 and point2
        /// </summary>
        private void UpdateLineRenderers()
        {
            Vector3 P1Poss = Point1.GetPointLocation();
            Vector3 P2Poss = Point2.GetPointLocation();
            lineRenderer.SetPosition(0, new Vector3(P1Poss.x, P1Poss.y, 0));
            lineRenderer.SetPosition(1, new Vector3(P2Poss.x, P2Poss.y, 0));
        }
        /// <summary>
        /// Spawns a new line
        /// </summary>
        private void StartLine()
        {
            SavedReff.IsSpawningLines = true;
            Point2.GetComponent<Collider2D>().enabled = true;
            IsFixed = true;
            IsMoving = false;
            LineDraggerC.StartLine(NodeRoot,this, LineColor);
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
            if (AttachedToNodeReff != null)
            {
                AttachedToNodeReff.LineAttacherC.DeatachLine();
                AttachedToNodeReff = null;
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
            if (!IsDragging  && !SavedReff.IsSpawningLines)
            {
                StartLine();
            }
        }
        public void DestroyLine()
        {
            SavedReff.RemoveActiveLine(this.gameObject);
            LineDraggerC.DeleteLines();
            Destroy(gameObject);
        }
        public void SetLineColor(Color lineColor)
        {
            LineColor = lineColor;
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
        }
        public void ChangePoint1(Vector2 newLoc)
        {
            Point1.MoveObj(new Vector3(newLoc.x, newLoc.y, 0));
        }
        public void ChangePoint2(Vector2 newLoc)
        {
            Point2.MoveObj(new Vector3(newLoc.x, newLoc.y, 0));
        }
    }
}