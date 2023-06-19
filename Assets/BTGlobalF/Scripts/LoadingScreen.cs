using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Animator RotatingImage;


        public void ClearLoadingSCreen()
        {
            Destroy(this.gameObject);
        }
    }
}
