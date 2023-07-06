using BehaviorTreePlanner.Global;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance;
        public GameObject SettingsScreen;
        public GameObject MainUiCanvas;

        private GameObject loadingScreen;

        [HideInInspector] public static string settingsfilepath { get;set; } = "";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Screen.SetResolution(1280, 720, false);

            DontDestroyOnLoad(gameObject);
            settingsfilepath = @$"{Application.dataPath}/BTSettings.st";
            LoadSettingsFromFile();
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
            try
            {
                BinaryFormatter bf = new();
                SavedSettings settings = new();
                if (Directory.Exists(settingsfilepath))
                {
                    Directory.Delete(settingsfilepath);
                }
                using (FileStream fs = new(settingsfilepath, FileMode.Create))
                {
                    bf.Serialize(fs, settings);
                }
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }

        }

        public void LoadSettingsFromFile()
        {
            BinaryFormatter bf = new();
            SavedSettings settings = new();
            try
            {
                if (File.Exists(settingsfilepath))
                {

                    using (FileStream fs = new(settingsfilepath, FileMode.Open))
                    {
                        settings = (SavedSettings)bf.Deserialize(fs);
                        BTSettings.NodeGridSize.x = settings.NodeGridSize[0];
                        BTSettings.NodeGridSize.y = settings.NodeGridSize[1];
                        BTSettings.LineGridSize.x = settings.LineGridSize[0];
                        BTSettings.LineGridSize.y = settings.LineGridSize[1];
                        BTSettings.SoundVolume = settings.SoundVolume;
                    }
                }
                else
                {
                   SaveSettingsToFile();
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
