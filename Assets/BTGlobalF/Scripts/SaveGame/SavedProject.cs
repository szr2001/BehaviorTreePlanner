using BehaviorTreePlanner.Nodes;
using System;
using System.Collections.Generic;

namespace BehaviorTreePlanner.Global
{
    [Serializable]
    public class SavedProject
    {
        public DateTime Date = DateTime.MinValue;
        public string ProjectName = "";
        public int AppVersion = 0;
        public List<NodeDesign> NodeTypes = new();
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
