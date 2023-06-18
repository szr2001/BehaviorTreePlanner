using BehaviorTreePlanner.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedProject
    {
        public DateTime Date = DateTime.MinValue;
        public string ProjectName = "";
        public int AppVersion = 0;
        public List<NodeDesign> NodeTypes = new(); // add in savenodebase and index representing this list
        public List<SavedProjectLayer> Layers = new();

        public SavedProject(SavedProjectLayer Default, int appVersion, string name, DateTime date)
        {
            AppVersion = appVersion;
            ProjectName = name;
            Layers.Add(Default);
            Date = date;
        }
    }
}