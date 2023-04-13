using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedLinePoint
    {
        public int Index;
        public float[] Position = new float[3];

        public float[] LineRendererPos1 = new float[3];
        public float[] LineRendererPos2 = new float[3];

    }
}
