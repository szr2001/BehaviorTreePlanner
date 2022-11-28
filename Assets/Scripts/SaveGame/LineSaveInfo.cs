using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner.SaveGame
{
    [Serializable]
    public class LineSaveInfo
    {
        public int[] lineLocation= new int[2];
        public int[] point1Location = new int[2];
        public int[] point2Location = new int[2];
        public int spawnedLineindex;

    }
}
