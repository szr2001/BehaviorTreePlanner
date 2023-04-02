using BehaviorTreePlanner.Global;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class MoveObjectsManager : MonoBehaviour
    {
        private List<IMovable> MovableObj = new();
        private bool IsMoving = false;

        private void Update()
        {
            if(IsMoving && MovableObj.Count > 0)
            {
                foreach (IMovable movableObj in MovableObj)
                {
                    Vector2 mospos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                    movableObj.MoveObj(mospos);
                    if(Input.GetMouseButtonUp(0))
                    {
                        IsMoving = false;
                        ClearMovableObj();
                        break;
                    }
                }
            }
        }
        public void StartMoving()
        {
            IsMoving = true;
        }
        public void StopMoving()
        {
            IsMoving = false;
        }
        public void AddMovableObj(IMovable Obj)
        {
            MovableObj.Add(Obj);
            Obj.Select();
        }
        public void RemoveMovableObj(IMovable Obj)
        {
            MovableObj.Remove(Obj);
            Obj.Deselect();
        }
        public void ClearMovableObj()
        {
            foreach(IMovable obj in MovableObj)
            {
                obj.Deselect();
            }
            MovableObj.Clear();
        }
    }
}
