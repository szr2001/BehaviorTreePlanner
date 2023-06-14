using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class LayerNodeButton : MonoBehaviour
    {
        private SaveLoadManager saveLoadManager;
        [SerializeField] private GameObject nodeName;
        [SerializeField] private GameObject nodeLayerNr;
        
        public void InitializeNodeButton(SaveLoadManager saveloadmanager)
        {
            saveLoadManager = saveloadmanager;
        }
        public void CallLoadLayer()
        {

        }
    }
}
