using UnityEngine;

namespace BehaviorTreePlanner
{
    public class MenuManager : MonoBehaviour
    {
        public ProjectsManager ProjectsManager;
        public SettingsManager SettingsManager;

        private void Awake()
        {
            Screen.SetResolution(1280, 720, false);
        }
    }
}
