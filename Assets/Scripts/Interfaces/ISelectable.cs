using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public interface ISelectable
    {
        public void Select();
        public void Deselect();

    }
}
