using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedProject
    {
        public string ProjectName = "";
        public int AppVersion = 0;
        public List<SavedProjectLayer> Layers = new();

        public SavedProject(SavedProjectLayer Default, int appVersion, string name)
        {
            AppVersion = appVersion;
            ProjectName = name;
            Layers.Add(Default);
        }
    }
}
