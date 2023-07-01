using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class BlackBoard : NodeBase
    {
        public override void InitializeNode(NodeDesign nd, EditorManager editormanager)
        {
            base.InitializeNode(nd, editormanager);
        }

        public override void InitializeLoad(SavedNodeBase savedata, EditorManager editormanager)
        {
            base.InitializeLoad(savedata, editormanager);
        }

        public override void Load()
        {
            base.Load();
        }

        public override SavedNodeBase Save()
        {
            return new SavedNodeBase
                (
                    -1,
                    lineHandler.AttachedPoint != null ? lineHandler.AttachedPoint.SaveIndex : -1,
                    lineHandler.SpawnedPoint != null ? lineHandler.SpawnedPoint.SaveIndex : -1
                );
        }
    }
}