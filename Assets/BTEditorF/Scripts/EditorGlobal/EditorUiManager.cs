using BehaviorTreePlanner.MenuUi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class EditorUiManager : MonoBehaviour
    {
        public static EditorUiManager Instance;
        public GameObject MainUiCanvas;
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
        }

        public NodesMenu NodeMenu;
        public LayersMenu LayerMenu;
    }
}
