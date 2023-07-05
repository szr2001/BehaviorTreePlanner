using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SettingsManager : MonoBehaviour
    {
        public GameObject SettingsScreen;
        public GameObject MainUiCanvas;

        private GameObject loadingScreen;

        private static string settingsfilepath = @"";
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void ShowSettingsScreen()
        {
            if (loadingScreen != null)
            {
                return;
            }
            loadingScreen = Instantiate(SettingsScreen, MainUiCanvas.transform);
            loadingScreen.GetComponent<SettingsScreen>().InitializeSettingsScreen(this);
        }

        public void HideSettingsScreen()
        {
            if (loadingScreen == null)
            {
                return;
            }
            loadingScreen.GetComponent<SettingsScreen>().ClearLoadingScreen();
            loadingScreen = null;
        }
        public void SaveSettingsToFile()
        {

        }
        public void LoadSettingsFromFile()
        {

        }
    }
}
