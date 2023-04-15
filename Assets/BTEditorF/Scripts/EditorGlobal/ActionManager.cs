using BehaviorTreePlanner.Global;
using BehaviorTreePlanner.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

namespace BehaviorTreePlanner
{
    public class ActionManager : MonoBehaviour
    {
        public EditorManager EditorManager;
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
                mMovableCopyList = EditorManager.MoveObjectsManager.MovableIList;
                EditorManager.MoveObjectsManager.StopMoving();
            }
        }
        private void CheckDelete()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if(EditorManager.MoveObjectsManager.MovableIList.Count > 0)
                {
                    foreach (IMovable m in EditorManager.MoveObjectsManager.MovableIList)
                    {
                        m.GetGameObj.GetComponent<IObjDestroyable>().DestroyObject();
                    }
                    EditorManager.MoveObjectsManager.ClearMovableObj();
                }
            }
        }
        private void CheckPaste()
        {
            if (IsPressingLeftCtrl && Input.GetKeyDown(KeyCode.V))
            {
                if(mMovableCopyList.Count < 0)
                {
                    return;
                }

                if (CheckNodePresence())
                {
                    Debug.Log("Paste");

                    //ToDo: Paste

                    //foreach(IMovable move in mMovableCopyList)
                    //{
                    //    SavedReff.MoveObjectsManager.AddMovableObj(move);
                    //    SavedReff.MoveObjectsManager.StartMoving();
                    //}
                }
            }
        }

        //helper functions
        private bool CheckNodePresence()
        {
            bool IsNodePreseant = false;
            foreach (IMovable Mobj in mMovableCopyList)
            {
                if (Mobj.GetGameObj.TryGetComponent<NodeBase>(out _))
                {
                    IsNodePreseant = true;
                    break;
                }
            }
            return IsNodePreseant;
        }
    }
}
