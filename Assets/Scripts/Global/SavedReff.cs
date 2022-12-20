using BehaviorTreePlanner.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public static class SavedReff
    {
        public static NodeManager NodeManager;
        public static Camera PlayerCamera;
        public static GameObject Screen;
        public static GameObject LinePrefabReff;
        public static GameObject NodePrefabReff;
        public static GameObject NodeButtonPrefabReff;
        public static GameObject RootNode;
        public static GameObject NodesUiMenu;
        public static GameObject MoveSelection;
        public static SettingsManager SettingsManager;

        public static bool IsSpawningNodes = false;
        public static bool IsSpawningLines = false;

        public static List<GameObject> ActiveNodes = new List<GameObject>();
        public static List<GameObject> ActiveLines = new List<GameObject>();
        public static void AddActiveNode(GameObject node)
        {
            ActiveNodes.Add(node);
        }
        public static void RemoveActiveNode(GameObject node)
        {
            ActiveNodes.Remove(node);
        }
        public static void AddActiveLine(GameObject line)
        {
            ActiveLines.Add(line);
        }
        public static void RemoveActiveLine(GameObject line)
        {
            ActiveLines.Remove(line);
        }
    }
}