using BehaviorTreePlanner.Global;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner.MenuUi
{
    public class UiHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            SavedReff.IsOverUi = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SavedReff.IsOverUi = false;
        }
    }
}
