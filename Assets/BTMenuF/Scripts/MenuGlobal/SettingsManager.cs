using BehaviorTreePlanner.MenuUi;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance;
        private BTLogger mLogger;
        public GameObject SettingsScreen;
        public GameObject MainUiCanvas;

        private GameObject loadingScreen;

        public delegate void BtSettingsChanged();
        public event BtSettingsChanged OnSettingsChanged;

        [HideInInspector] public static string settingsfilepath { get; set; } = "";

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
            mLogger = new(this.name, false);

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
            OnSettingsChanged?.Invoke();
        }

        public void SaveSettingsToFile()
        {
            mLogger.Log("SaveSettingsToFile", $"Start Saving settings");
            try
            {
                mLogger.Log("SaveSettingsToFile", $"Saving settings to file");
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
            catch (Exception ex)
            {
                mLogger.Log("SaveSettingsToFile", $" Saving settings EXCEPTION TRHOWN:  {ex.Message}");
                Debug.LogException(ex);
            }
        }

        public void LoadSettingsFromFile()
        {
            BinaryFormatter bf = new();
            SavedSettings settings = new();
            mLogger.Log("LoadSettingsFromFile", "Start loading settings");
            try
            {
                if (File.Exists(settingsfilepath))
                {
                    mLogger.Log("LoadSettingsFromFile", $"Loading settings from file");
                    using (FileStream fs = new(settingsfilepath, FileMode.Open))
                    {
                        settings = (SavedSettings)bf.Deserialize(fs);
                        BTSettings.NodeGridSize.x = settings.NodeGridSize[0];
                        BTSettings.NodeGridSize.y = settings.NodeGridSize[1];
                        BTSettings.LineGridSize.x = settings.LineGridSize[0];
                        BTSettings.LineGridSize.y = settings.LineGridSize[1];
                        BTSettings.OverallSoundVolume = settings.OverallSoundVolume;
                        BTSettings.AtmosphericSound = settings.AtmosphericSound;
                        BTSettings.EffectsSound = settings.EffectsSound;
                    }
                }
                else
                {
                    mLogger.Log("LoadSettingsFromFile", $"Settings file missing");
                    SaveSettingsToFile();
                }
            }
            catch (Exception ex)
            {
                mLogger.Log("LoadSettingsFromFile", $"Loading settings EXCEPTION TRHOWN:  {ex.Message}");
                Debug.LogException(ex);
            }
        }
    }
}
