using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    [Serializable]
    public static class SavedSettings
    {
        public static Vector2 LineGridSize = new (0.5f, 0.5f);
        public static Vector2 NodeGridSize = new (1.5f, 1);
    }
}
