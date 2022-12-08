using BehaviorTreePlanner.Lines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class LineDraggerClass
    {
        public List<Line> Spawnedlines { get; } = new List<Line>();
        public GameObject Parent { get; set; }
        public GameObject LineTrigger { get; set; }

        public LineDraggerClass(GameObject parent, GameObject lineTrigger)
        {
            this.Parent = parent;
            this.LineTrigger = lineTrigger;
        }
        public void StartLine()
        {
            GameObject LinePrefab = GameObject.Instantiate(SavedReff.LinePrefabReff, Parent.transform);
            LinePrefab.transform.SetParent(SavedReff.Screen.transform);
            LinePrefab.transform.localScale = Vector3.one;
            Line spawnedLine = LinePrefab.GetComponent<Line>();
            spawnedLine.ChangePoint1(LineTrigger.transform.position);
            Spawnedlines.Add(spawnedLine);
        }
        public void DeleteLines()
        {
             if (Spawnedlines.Count > 0)
             {
                foreach (Line line in Spawnedlines)
                {
                    if (line != null)
                    {
                        line.DestroyLine();
                    }
                }
             }
        }
        public void SetLinesLocation()
        {
            if (Spawnedlines.Count > 0)
            {
                foreach (Line line in Spawnedlines)
                {
                    if (line != null)
                    {
                        line.ChangePoint1(LineTrigger.transform.position);
                    }
                }
            }
        }
    }
}
