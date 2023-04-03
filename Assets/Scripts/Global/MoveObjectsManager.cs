using BehaviorTreePlanner.Global;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class MoveObjectsManager : MonoBehaviour
    {
        private Dictionary<IMovable,Vector3> MovableObj = new();
        private bool IsMoving = false;

        private void Update()
        {
            if(IsMoving && MovableObj.Count > 0)
            {
                foreach (KeyValuePair<IMovable, Vector3> movableObj in MovableObj)
                {
                    Vector2 mospos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                    movableObj.Key.MoveObj(mospos, movableObj.Value);
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
            if(MovableObj.Count > 1)
            {
                Vector3 MosPoss = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                Dictionary<IMovable, Vector3> MovableObjTemp = new();
                foreach (KeyValuePair<IMovable, Vector3> Obj in MovableObj) 
                {
                    MovableObjTemp.Add(Obj.Key, MosPoss - Obj.Key.GetStartPosition);
                }
                MovableObj = MovableObjTemp;
            }
            IsMoving = true;
        }
        public void StopMoving()
        {
            IsMoving = false;
        }
        public void AddMovableObj(IMovable Obj)
        {
            try
            {
                MovableObj.Add(Obj, Vector3.zero);
                Obj.Select();
            }
            catch { }
        }
        public void RemoveMovableObj(IMovable Obj)
        {
            MovableObj.Remove(Obj);
            Obj.Deselect();
        }
        public void ClearMovableObj()
        {
            foreach(KeyValuePair<IMovable, Vector3> obj in MovableObj)
            {
                obj.Key.Deselect();
            }
            MovableObj.Clear();
        }
    }
}
