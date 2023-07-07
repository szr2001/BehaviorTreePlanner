using System;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class BTSettings
    {
        public static Vector2 LineGridSize = new (0.5f, 0.5f);
        public static Vector2 NodeGridSize = new (1.5f, 1);
        public static int OverallSoundVolume = 100;
        public static int AtmosphericSound = 100;
        public static int EffectsSound = 100;
    }

    [Serializable]
    public class SavedSettings
    {
        public float[] LineGridSize = new float[2];
        public float[] NodeGridSize = new float[2];
        public int OverallSoundVolume;
        public int AtmosphericSound;
        public int EffectsSound;

        public SavedSettings()
        {
            LineGridSize[0] = BTSettings.LineGridSize.x;
            LineGridSize[1] = BTSettings.LineGridSize.y;
            NodeGridSize[0] = BTSettings.NodeGridSize.x;
            NodeGridSize[1] = BTSettings.NodeGridSize.y;
            OverallSoundVolume = BTSettings.OverallSoundVolume;
            AtmosphericSound = BTSettings.AtmosphericSound;
            EffectsSound = BTSettings.EffectsSound;
        }
    }
}
