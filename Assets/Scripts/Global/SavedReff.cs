using BehaviorTreePlanner.Nodes;
using BehaviorTreePlanner.Player;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public static class SavedReff
    {
        //--------------------Game Refferencess--------------------------//
        public static Camera PlayerCamera;
        public static GameObject Screen;
        public static GameObject NodePrefabReff;
        public static GameObject LinePointPrefabReff;
        public static GameObject NodeButtonPrefabReff;
        public static GameObject RootNode;
        public static GameObject NodesUiMenu;
        public static SettingsManager SettingsManager;
        public static MoveObjectsManager MoveObjectsManager;
        public static SpawnManager SpawnManager;
        public static SoundManager SoundManager;
        public static ActionManager ActionManager;
        public static CameraControl PlayerControll;
        public static SaveLoadManager SaveLoadManager;

        public static bool IsOverUi = false;
        
        //--------------Active Scene objects refference------------------//

        public static List<GameObject> ActiveNodes = new();
        public static List<GameObject> ActiveLines = new();
        public static List<GameObject> ActiveProjectNodes = new();

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

        //----------------------Helper Functions-------------------------//
        /// <summary>
        /// Converts mouse position to a grid position
        /// </summary>
        public static Vector2 MousePositionToGrid(Vector2 MousePos,Vector2 GridSize , Vector2 Offset,Vector2 OptionalOffset) 
        {
            Vector2 GridPosition  = new Vector2(MousePos.x, MousePos.y) - Offset;
            GridPosition.x = Mathf.Round(GridPosition.x / GridSize.x) * GridSize.x;
            GridPosition.y = Mathf.Round(GridPosition.y / GridSize.y) * GridSize.y;
            GridPosition -= OptionalOffset;
            return GridPosition;
        }
    }
}