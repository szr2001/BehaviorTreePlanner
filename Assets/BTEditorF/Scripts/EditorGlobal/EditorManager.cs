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
        public ProjectsManager ProjectsManager;

        [HideInInspector]public bool IsOverUi = false;
        private void Awake()
        {
            ProjectsManager = GameObject.FindObjectOfType<ProjectsManager>();
        }
    }
}
