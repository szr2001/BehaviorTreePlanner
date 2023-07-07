using BehaviorTreePlanner.MenuUi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class EditorUiManager : MonoBehaviour
    {
        public NodesMenu NodeMenu;
        public LayersMenu LayerMenu;
        public static EditorUiManager Instance;
        public GameObject MainUiCanvas;
        public AudioSource AudioAtmosphere;
        public AudioSource AudioEffects;
        [HideInInspector] public bool IsOverUi = false;

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
            SettingsManager.Instance.MainUiCanvas = MainUiCanvas;
            SoundManager.Instance.AudioAtmosphere = AudioAtmosphere;
            SoundManager.Instance.AudioEffects = AudioEffects;
            SoundManager.Instance.SetAudioVolumes();
        }
    }
}
