using BehaviorTreePlanner.Global;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTreePlanner
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

            OverallSoundSlider.value = (float)BTSettings.OverallSoundVolume;
            OverallSoundValue.text = OverallSoundSlider.value.ToString();

            AtmosphericSoundSlider.value = (float)BTSettings.AtmosphericSound;
            AtmoshpericSoundValue.text = AtmosphericSoundSlider.value.ToString();

            EffectsSoundSlider.value = (float)BTSettings.EffectsSound;
            EffectsSoundValue.text = EffectsSoundSlider.value.ToString();

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
            BTSettings.OverallSoundVolume = (int)newvalue;
            OverallSoundValue.text = OverallSoundSlider.value.ToString();
        }
        public void ChangedAtmosphericVolume(float newvalue)
        {
            BTSettings.AtmosphericSound = (int)newvalue;
            AtmoshpericSoundValue.text = OverallSoundSlider.value.ToString();
        }
        public void ChangedEffectsVolume(float newvalue)
        {
            BTSettings.EffectsSound = (int)newvalue;
            EffectsSoundValue.text = OverallSoundSlider.value.ToString();
        }
        public void CloseSelf()
        {
            settingsManager.HideSettingsScreen();
        }
    }
}
