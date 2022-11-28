using BehaviorTreePlanner.Nodes;
using UnityEngine;

namespace BehaviorTreePlanner.Global
{
    public class ReffSaverManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject screenReff;
        [SerializeField] private GameObject linePrefabReff;
        [SerializeField] private GameObject nodePrefabReff;
        [SerializeField] private GameObject nodeButtonPrefabReff;
        [SerializeField] private GameObject screenNodeRoot;
        [SerializeField] private GameObject nodesUiMenu;
        [SerializeField] private NodeManager nodeManager;
        [SerializeField] private SettingsManager settingsManager;

        private void Awake()
        {
            SavedReff.Screen = screenReff;
            SavedReff.Player = player;
            SavedReff.LinePrefabReff = linePrefabReff;
            SavedReff.NodePrefabReff = nodePrefabReff;
            SavedReff.NodeButtonPrefabReff = nodeButtonPrefabReff;
            SavedReff.RootNode = screenNodeRoot;
            SavedReff.NodesUiMenu = nodesUiMenu;
            SavedReff.NodeManager = nodeManager;
            SavedReff.SettingsManager = settingsManager;
        }

    }
}
