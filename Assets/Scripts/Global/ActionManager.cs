using BehaviorTreePlanner.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BehaviorTreePlanner
{
    public class ActionManager : MonoBehaviour
    {
        private List<IMovable> mMovableCopyList = new();
        private bool IsPressingLeftCtrl = false;
        private void Update()
        {
            CheckPressingLeftControl();
            CheckDelete();
            CheckCopy();
            CheckPaste();
        }
        private void CheckPressingLeftControl()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                IsPressingLeftCtrl = true;
            }
            else
            {
                IsPressingLeftCtrl = false;
            }
        }
        private void CheckCopy()
        {
            if (IsPressingLeftCtrl && Input.GetKeyDown(KeyCode.C))
            {
                mMovableCopyList = SavedReff.MoveObjectsManager.MovableIList;
                SavedReff.MoveObjectsManager.StopMoving();
            }
        }
        private void CheckDelete()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if(SavedReff.MoveObjectsManager.MovableIList.Count > 0)
                {
                    foreach (IMovable m in SavedReff.MoveObjectsManager.MovableIList)
                    {
                        m.GetGameObj.GetComponent<IObjDestroyable>().DestroyObject();
                    }
                    SavedReff.MoveObjectsManager.ClearMovableObj();
                }
            }
        }
        private void CheckPaste()
        {

        }
    }
}
