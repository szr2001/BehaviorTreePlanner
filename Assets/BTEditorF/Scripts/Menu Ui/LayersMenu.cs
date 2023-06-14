using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class LayersMenu : MonoBehaviour
    {
        [SerializeField] private SaveLoadManager saveLoadManager;
        // Start is called before the first frame update
        void Start()
        {
            //spawn buttons for each layer in OppenedProject and highlight layer 0
            //set their saveloadmanager to this.saveloadmanager
        }

        private void SpawnLayerButtons()
        {
            
        }
    }
}
