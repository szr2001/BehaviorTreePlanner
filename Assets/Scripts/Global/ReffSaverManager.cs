using BehaviorTreePlanner.Nodes;
using BehaviorTreePlanner.Player;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class ReffSaverManager : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private GameObject screenReff;
        [SerializeField] private GameObject nodePrefabReff;
        [SerializeField] private GameObject linePointPrefabReff;
        [SerializeField] private GameObject nodeButtonPrefabReff;
        [SerializeField] private GameObject screenNodeRoot;
        [SerializeField] private GameObject nodesUiMenu;
        [SerializeField] private SettingsManager settingsManager;
        [SerializeField] private MoveObjectsManager moveObjectsManager;
        [SerializeField] private SpawnManager spawnManager;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private ActionManager actionManager;
        [SerializeField] private SaveLoadManager saveLoadManager;

        private void Awake()
        {
            SavedReff.Screen = screenReff;
            SavedReff.PlayerCamera = playerCamera;
            SavedReff.NodePrefabReff = nodePrefabReff;
            SavedReff.NodeButtonPrefabReff = nodeButtonPrefabReff;
            SavedReff.RootNode = screenNodeRoot;
            SavedReff.NodesUiMenu = nodesUiMenu;
            SavedReff.SettingsManager = settingsManager;
            SavedReff.LinePointPrefabReff = linePointPrefabReff;
            SavedReff.MoveObjectsManager = moveObjectsManager;
            SavedReff.SpawnManager = spawnManager;
            SavedReff.SoundManager = soundManager;
            SavedReff.ActionManager = actionManager;
            SavedReff.SaveLoadManager = saveLoadManager;
        }

    }
}
