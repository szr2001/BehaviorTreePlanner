using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    [Serializable]
    public class SavedLinePoint
    {
        //use List index of nodes/lines from savedref for savedline/savednode index
        public int LineIndex;
        public float[] Position = new float[2];

        public float[] LineRendererPos1 = new float[2];
        public float[] LineRendererPos2 = new float[2];

        public int ParentLineIndex;
        public int[] SpawnedLinesIndex;

        public SavedLinePoint(int lineIndex, float[] position, float[] lineRendererPos1, float[] lineRendererPos2, int parentLineIndex, int[] spawnedLinesIndex)
        {
            LineIndex = lineIndex;
            Position = position;
            LineRendererPos1 = lineRendererPos1;
            LineRendererPos2 = lineRendererPos2;
            ParentLineIndex = parentLineIndex;
            SpawnedLinesIndex = spawnedLinesIndex;
        }
    }
}
