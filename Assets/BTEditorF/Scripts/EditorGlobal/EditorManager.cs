using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class EditorManager : MonoBehaviour
    {
        public GameObject NodesUiMenu;
        public CameraControl PlayerControll;
        public SettingsManager SettingsManager;
        public MoveObjectsManager MoveObjectsManager;
        public SpawnManager SpawnManager;
        public SoundManager SoundManager;
        public ActionManager ActionManager;
        public SaveLoadManager SaveLoadManager;

        [HideInInspector]public bool IsOverUi = false;

    }
}
