using BehaviorTreePlanner.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class MoveObjectsManager : MonoBehaviour
    {
        private Dictionary<IMovable, Vector3> MovableObj = new();
        public int MoveObjCount { get { return MovableObj.Count; } }

        private bool IsMoving = false;

        public List<IMovable> MovableIList { get {return MovableObj.Keys.ToList(); } }
        private void Update()
        {
            if(IsMoving && MovableObj.Count > 0)
            {
                foreach (KeyValuePair<IMovable, Vector3> movableObj in MovableObj)
                {
                    Vector2 mospos = SavedReff.PlayerCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);
                    movableObj.Key.MoveObj(mospos, movableObj.Value,true);
                    if(Input.GetMouseButtonUp(0))
                    {
                        StopMoving();
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
                    MovableObjTemp.Add(Obj.Key, MosPoss - Obj.Key.GetObjPosition);
                }
                MovableObj = MovableObjTemp;
            }
            IsMoving = true;
        }
        public void StopMoving()
        {
            IsMoving = false;
            ClearMovableObj();
        }
        public void AddMovableObj(IMovable Obj)
        {
            try
            {
                MovableObj.Add(Obj, Vector3.zero);
                Obj.StartMoveObj();
            }
            catch{}
        }
        public void RemoveMovableObj(IMovable Obj)
        {
            MovableObj.Remove(Obj);
            Obj.StopMoveObj();
        }
        public void ClearMovableObj()
        {
            foreach(KeyValuePair<IMovable, Vector3> obj in MovableObj)
            {
                obj.Key?.StopMoveObj();
            }
            MovableObj.Clear();
        }
    }
}
