using BehaviorTreePlanner.Global;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
{
    public class SettingsScreen : MonoBehaviour
    {
        public SettingsManager SettingsManager;
        public Slider SoundSlider;
        public Text SoundValue;

        public void InitializeSettingsScreen(SettingsManager settingsmanager)
        {
            SettingsManager = settingsmanager;
            SoundSlider.value = (float)SavedSettings.SoundVolume;
            SoundValue.text = SoundSlider.value.ToString();
            SoundSlider.onValueChanged.AddListener(ChangedSoundVolume);
        }
        public void ClearLoadingScreen()
        {
            Destroy(this.gameObject);
        }
        public void ChangedSoundVolume(float newvalue)
        {
            SavedSettings.SoundVolume = (int)newvalue;
            SoundValue.text = SoundSlider.value.ToString();
        }
        public void CloseSelf()
        {
            SettingsManager.HideSettingsScreen();
        }
    }
}
