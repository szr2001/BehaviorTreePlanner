using BehaviorTreePlanner.Player;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class EditorManager : MonoBehaviour
    {
        public GameObject MainUiCanvas;

        public CameraControl PlayerControll;
        public MoveObjectsManager MoveObjectsManager;
        public SpawnManager SpawnManager;
        public SoundManager SoundManager;
        public ActionManager ActionManager;
        public SaveLoadManager SaveLoadManager;
        public EditorUiManager EditorUiManager;
        public ProjectsManager ProjectsManager;
        public SettingsManager SettingsManager;

        [HideInInspector]public bool IsOverUi = false;
        private void Awake()
        {
            ProjectsManager = GameObject.FindObjectOfType<ProjectsManager>();
            SettingsManager = GameObject.FindObjectOfType<SettingsManager>();
            SettingsManager.MainUiCanvas = MainUiCanvas;
        }
    }
}
