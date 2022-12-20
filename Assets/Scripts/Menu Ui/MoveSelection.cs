using UnityEngine;
using BehaviorTreePlanner.Nodes;
using System.Collections.Generic;

namespace BehaviorTreePlanner.Global
{
    public class MoveSelection : MonoBehaviour
    {
        private List<GameObject> OverlapedObjects = new List<GameObject>();
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CheckOverlap(Bounds Area)
        {
            OverlapedObjects.Clear();
            foreach (GameObject G in SavedReff.ActiveNodes)
            {
                if (IsPointInsideArea(SavedReff.PlayerCamera.WorldToScreenPoint(G.transform.position), Area))
                {
                    OverlapedObjects.Add(G);
                }
            }
        }
        private bool IsPointInsideArea(Vector2 poss, Bounds area)
        {
            return poss.x > area.min.x && poss.x < area.max.x
                && poss.y > area.min.y && poss.y < area.max.y;
        }
    }
}
