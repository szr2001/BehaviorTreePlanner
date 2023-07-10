using System;

namespace BehaviorTreePlanner.Global
{
    [Serializable]
    public class SavedLinePoint
    {
        //use List index of nodes/lines from savedref for savedline/savednode index
        public int LineIndex;
        public byte IsRoot;
        public byte IsAtachedToNode;
        public float[] Position = new float[2];

        public float[] LineRendererPos1 = new float[2];
        public float[] LineRendererPos2 = new float[2];

        public int ParentLineIndex;
        public int AtachedToNodeIndex;
        public int[] SpawnedLinesIndex;

        public SavedLinePoint(byte isroot, int lineIndex, float[] position, float[] lineRendererPos1, float[] lineRendererPos2, int parentLineIndex, int[] spawnedLinesIndex, int atachedtonodeIndex, byte isatachedtonode)
        {
            LineIndex = lineIndex;
            Position = position;
            LineRendererPos1 = lineRendererPos1;
            LineRendererPos2 = lineRendererPos2;
            ParentLineIndex = parentLineIndex;
            SpawnedLinesIndex = spawnedLinesIndex;
            IsRoot = isroot;
            AtachedToNodeIndex = atachedtonodeIndex;
            IsAtachedToNode = isatachedtonode;
        }
    }
}
