using BehaviorTreePlanner.Nodes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BehaviorTreePlanner.SaveGame
{
    public static class SaveGameManager
    {
        private static string saveFolderName = "SaveFiles";
        private static string saveFilesLocation = $"{Application.dataPath}/{saveFolderName}";
        private static string saveProjectFilesLocation = $"{saveFilesLocation}/SavedProjects";
        private static List<GameObject> SpawnedLines = new List<GameObject>();
        private static List<GameObject> SpawnedNodes = new List<GameObject>();
        //every save will be a folder with node types file and project file
        public static void SaveGame(string saveFileName)
        {
            if (!Directory.Exists(saveFilesLocation))
            {
                Directory.CreateDirectory(saveFilesLocation);
                Directory.CreateDirectory(saveProjectFilesLocation);
            }
            PrepareSaveGame();
        }
        public static void LoadGame()
        {

        }
        
        public static void PrepareSaveGame()
        {

        }

        public static void SaveCustomNodeTypes()
        {

        }
        public static void LoadCustomNodeTypes()
        {

        }
    }
}