using BehaviorTreePlanner.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace BehaviorTreePlanner
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        public AudioSource AudioAtmosphere;
        public AudioSource AudioEffects;

        [Header("Sound Types")]
        public AudioClip BaloonPop;
        public AudioClip ButtonPop;
        public AudioClip HardPop;
        public AudioClip WetPop;

        private float MaxAtmosphereSound = 0.05f;
        private float MaxEffectsSound = 1f;

        private float AtmosphericSounds;
        private float EffectsSound;
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

            DontDestroyOnLoad(gameObject);
            SettingsManager.Instance.OnSettingsChanged += SetAudioVolumes;

            SetAudioVolumes();
        }
        
        public void SetAudioVolumes()
        {
            AtmosphericSounds = MaxAtmosphereSound * BTSettings.AtmosphericSound * BTSettings.OverallSoundVolume;
            EffectsSound = MaxEffectsSound * BTSettings.EffectsSound * BTSettings.OverallSoundVolume;
            AudioAtmosphere.volume = AtmosphericSounds;
            AudioEffects.volume = EffectsSound;
        }

        public void ChangeAtmosphericSoundAplitude(float volume)
        {
            AudioAtmosphere.volume = volume;
        }

        public void PlayBaloonPop()
        {
            if (AudioEffects.isPlaying)
            {
                return;
            }
            AudioEffects.clip = BaloonPop;
            AudioEffects.Play();
        }

        public void PlayButtonPop()
        {
            if (AudioEffects.isPlaying)
            {
                return;
            }
            AudioEffects.clip = ButtonPop;
            AudioEffects.Play();
        }

        public void PlayHardPop()
        {
            if (AudioEffects.isPlaying)
            {
                return;
            }
            AudioEffects.clip = HardPop;
            AudioEffects.Play();
        }

        public void PlayWetPop()
        {
            if (AudioEffects.isPlaying)
            {
                return;
            }
            AudioEffects.clip = WetPop;
            AudioEffects.Play();
        }
    }
}
