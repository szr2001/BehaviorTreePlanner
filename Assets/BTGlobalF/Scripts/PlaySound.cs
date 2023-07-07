using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BehaviorTreePlanner
{
    public class PlaySound : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerDownHandler,IPointerUpHandler
    {
        public Sound SelectedSound = Sound.WetPop;
        public Action SoundTrigger = Action.Hover;
        public void OnPointerClick(PointerEventData eventData)
        {
           if(SoundTrigger == Action.Click) 
           {
                CallPlaySound();
           }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (SoundTrigger == Action.PointerDown && eventData.button == PointerEventData.InputButton.Left)
            {
                CallPlaySound();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SoundTrigger == Action.Hover)
            {
                CallPlaySound();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (SoundTrigger == Action.PointerUp)
            {
                CallPlaySound();
            }
        }

        private void CallPlaySound()
        {
            switch (SelectedSound)
            {
                case Sound.BaloonPop:
                    SoundManager.Instance.PlayBaloonPop();
                    break;
                case Sound.ButtonPop:
                    SoundManager.Instance.PlayButtonPop();
                    break;
                case Sound.HardPop:
                    SoundManager.Instance.PlayHardPop();
                    break;
                case Sound.WetPop:
                    SoundManager.Instance.PlayWetPop();
                    break;

            }
        }

        public enum Sound
        {
            BaloonPop,
            ButtonPop,
            HardPop,
            WetPop
        }
        public enum Action
        {
            Click,
            Hover,
            PointerDown,
            PointerUp
        }
    }
}
