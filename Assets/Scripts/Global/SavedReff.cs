using BehaviorTreePlanner.Nodes;
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
        public static SettingsManager SettingsManager;

        public static bool IsSpawningNodes = false;
        public static bool IsSpawningLines = false;
    }
}