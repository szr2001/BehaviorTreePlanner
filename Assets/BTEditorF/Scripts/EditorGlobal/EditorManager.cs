using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class EditorManager : MonoBehaviour
    {
        public GameObject Screen;
        public GameObject NodePrefabReff; //move prefabb reff to managers
        public GameObject LinePointPrefabReff;
        public GameObject NodeButtonPrefabReff;
        public GameObject NodesUiMenu;
        public CameraControl PlayerControll;
        public SettingsManager SettingsManager;
        public MoveObjectsManager MoveObjectsManager;
        public SpawnManager SpawnManager;
        public SoundManager SoundManager;
        public ActionManager ActionManager;
        public SaveLoadManager SaveLoadManager;



        [HideInInspector]public bool IsOverUi = false;

        //--------------Active Scene objects refference------------------//

        public List<GameObject> ActiveNodes = new();
        public List<GameObject> ActiveLines = new();

        public void AddActiveNode(GameObject node)
        {
            if (!ActiveNodes.Contains(node))
            {
                ActiveNodes.Add(node);
            }
        }
        public void RemoveActiveNode(GameObject node)
        {
            if (ActiveNodes.Contains(node))
            {
                ActiveNodes.Remove(node);
            }
        }
        public void AddActiveLine(GameObject line)
        {
            if (!ActiveLines.Contains(line))
            {
                ActiveLines.Add(line);
            }
        }
        public void RemoveActiveLine(GameObject line)
        {
            if (ActiveLines.Contains(line))
            {
                ActiveLines.Remove(line);
            }
        }

        //----------------------Helper Functions-------------------------//
        /// <summary>
        /// Converts mouse position to a grid position
        /// </summary>
        public Vector2 MousePositionToGrid(Vector2 MousePos, Vector2 GridSize, Vector2 Offset, Vector2 OptionalOffset)
        {
            Vector2 GridPosition = new Vector2(MousePos.x, MousePos.y) - Offset;
            GridPosition.x = Mathf.Round(GridPosition.x / GridSize.x) * GridSize.x;
            GridPosition.y = Mathf.Round(GridPosition.y / GridSize.y) * GridSize.y;
            GridPosition -= OptionalOffset;
            return GridPosition;
        }
    }
}
