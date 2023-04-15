using BehaviorTreePlanner.Global;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner.MenuUi
{
    public class UiHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public EditorManager EditorManager;
        public void OnPointerEnter(PointerEventData eventData)
        {
            EditorManager.IsOverUi = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EditorManager.IsOverUi = false;
        }
    }
}
