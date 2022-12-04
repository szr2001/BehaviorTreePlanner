using BehaviorTreePlanner.Lines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class LineDraggerClass
    {
        private List<Line> _spawnedlines = new List<Line>();
        public List<Line> spawnedlines { get { return _spawnedlines; } }
        public GameObject parent { get; set; }
        public GameObject lineTrigger { get; set; }

        public LineDraggerClass(GameObject parent, GameObject lineTrigger)
        {
            this.parent = parent;
            this.lineTrigger = lineTrigger;
        }
        public void StartLine()
        {
            GameObject LinePrefab = GameObject.Instantiate(SavedReff.LinePrefabReff, parent.transform);
            LinePrefab.transform.SetParent(SavedReff.Screen.transform);
            LinePrefab.transform.localScale = Vector3.one;
            Line spawnedLine = LinePrefab.GetComponent<Line>();
            spawnedLine.ChangePoint1(lineTrigger.transform.position);
            _spawnedlines.Add(spawnedLine);
        }
        public void DeleteLines()
        {
             if (_spawnedlines.Count > 0)
             {
                foreach (Line line in _spawnedlines)
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
            if (_spawnedlines.Count > 0)
            {
                foreach (Line line in _spawnedlines)
                {
                    if (line != null)
                    {
                        line.ChangePoint1(lineTrigger.transform.position);
                    }
                }
            }
        }
    }
}
