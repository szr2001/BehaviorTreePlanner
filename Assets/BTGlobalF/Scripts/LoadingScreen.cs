using UnityEngine;

namespace BehaviorTreePlanner.MenuUi
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Animator RotatingImage;


        public void ClearLoadingScreen()
        {
            Destroy(this.gameObject);
        }
    }
}
