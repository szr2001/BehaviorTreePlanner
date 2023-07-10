using BehaviorTreePlanner.Global;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner.MenuUi
{
    public class SettingsScreen : MonoBehaviour
    {
        public SettingsManager settingsManager;

        public Slider OverallSoundSlider;
        public Text OverallSoundValue;

        public Slider AtmosphericSoundSlider;
        public Text AtmoshpericSoundValue;

        public Slider EffectsSoundSlider;
        public Text EffectsSoundValue;
        public void InitializeSettingsScreen(SettingsManager settingsmanager)
        {
            settingsManager = settingsmanager;

            OverallSoundSlider.value = BTSettings.OverallSoundVolume;
            OverallSoundValue.text = (OverallSoundSlider.value * 100).ToString();

            AtmosphericSoundSlider.value = BTSettings.AtmosphericSound;
            AtmoshpericSoundValue.text = (AtmosphericSoundSlider.value * 100).ToString();

            EffectsSoundSlider.value = BTSettings.EffectsSound;
            EffectsSoundValue.text = (EffectsSoundSlider.value * 100).ToString();

            OverallSoundSlider.onValueChanged.AddListener(ChangedOverallSoundVolume);
            AtmosphericSoundSlider.onValueChanged.AddListener(ChangedAtmosphericVolume);
            EffectsSoundSlider.onValueChanged.AddListener(ChangedEffectsVolume);
        }
        public void ClearLoadingScreen()
        {
            settingsManager.SaveSettingsToFile();
            Destroy(this.gameObject);
        }
        public void ChangedOverallSoundVolume(float newvalue)
        {
            BTSettings.OverallSoundVolume = newvalue;
            OverallSoundValue.text = ((int)(OverallSoundSlider.value * 100)).ToString();
        }
        public void ChangedAtmosphericVolume(float newvalue)
        {
            BTSettings.AtmosphericSound = newvalue;
            AtmoshpericSoundValue.text = ((int)(AtmosphericSoundSlider.value * 100)).ToString();
        }
        public void ChangedEffectsVolume(float newvalue)
        {
            BTSettings.EffectsSound = newvalue;
            EffectsSoundValue.text = ((int)(EffectsSoundSlider.value * 100)).ToString();
        }
        public void CloseSelf()
        {
            settingsManager.HideSettingsScreen();
        }
    }
}
