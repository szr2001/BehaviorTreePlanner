using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Lines;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class BlackBoard : MonoBehaviour
    {
        private LineDraggerClass LineDraggerC;
        [SerializeField] private GameObject lineTrigger;

        private void Awake()
        {
            LineDraggerC = new LineDraggerClass(gameObject, lineTrigger);
        }
        public void StartLine()
        {
            LineDraggerC.StartLine();
        }
    }
}