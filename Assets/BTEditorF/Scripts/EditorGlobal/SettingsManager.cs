using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class SettingsManager : MonoBehaviour
    {
        private void Awake()
        {
            Screen.SetResolution(1280, 720, false);
        }
    }
}
