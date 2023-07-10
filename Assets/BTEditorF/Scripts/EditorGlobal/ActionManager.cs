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
        public static ActionManager Instance;
        private BTLogger mLogger;
        private List<IMovable> mMovableCopyList = new();
        private bool IsPressingLeftCtrl = false;
        private void Update()
        {
            CheckPressingLeftControl();
            CheckDelete();
            //CheckCopy();
            //CheckPaste();
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            mLogger = new(this.name,false);
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
                mMovableCopyList = MoveObjectsManager.Instance.MovableIList;
                MoveObjectsManager.Instance.StopMoving();
            }
        }
        private void CheckDelete()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if(MoveObjectsManager.Instance.MovableIList.Count > 0)
                {
                    mLogger.Log("CheckDelete",$"Deleted {MoveObjectsManager.Instance.MovableIList.Count} elements");

                    foreach (IMovable m in MoveObjectsManager.Instance.MovableIList)
                    {
                        m.GetGameObj.GetComponent<IObjDestroyable>().DestroyObject();
                    }
                    MoveObjectsManager.Instance.ClearMovableObj();
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
