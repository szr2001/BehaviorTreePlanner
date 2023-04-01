using BehaviorTreePlanner.Nodes;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class ReffSaverManager : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private GameObject screenReff;
        [SerializeField] private GameObject linePrefabReff;
        [SerializeField] private GameObject nodePrefabReff;
        [SerializeField] private GameObject nodeButtonPrefabReff;
        [SerializeField] private GameObject screenNodeRoot;
        [SerializeField] private GameObject nodesUiMenu;
        [SerializeField] private SettingsManager settingsManager;

        private void Awake()
        {
            SavedReff.Screen = screenReff;
            SavedReff.PlayerCamera = playerCamera;
            SavedReff.LinePrefabReff = linePrefabReff;
            SavedReff.NodePrefabReff = nodePrefabReff;
            SavedReff.NodeButtonPrefabReff = nodeButtonPrefabReff;
            SavedReff.RootNode = screenNodeRoot;
            SavedReff.NodesUiMenu = nodesUiMenu;
            SavedReff.SettingsManager = settingsManager;
        }

    }
}
