using BehaviorTreePlanner.Global;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class SettingsScreen : MonoBehaviour
    {
        public SettingsManager settingsManager;
        public Slider SoundSlider;
        public Text SoundValue;

        public void InitializeSettingsScreen(SettingsManager settingsmanager)
        {
            settingsManager = settingsmanager;
            SoundSlider.value = (float)BTSettings.SoundVolume;
            SoundValue.text = SoundSlider.value.ToString();
            SoundSlider.onValueChanged.AddListener(ChangedSoundVolume);
        }
        public void ClearLoadingScreen()
        {
            settingsManager.SaveSettingsToFile();
            Destroy(this.gameObject);
        }
        public void ChangedSoundVolume(float newvalue)
        {
            BTSettings.SoundVolume = (int)newvalue;
            SoundValue.text = SoundSlider.value.ToString();
        }
        public void CloseSelf()
        {
            settingsManager.HideSettingsScreen();
        }
    }
}
