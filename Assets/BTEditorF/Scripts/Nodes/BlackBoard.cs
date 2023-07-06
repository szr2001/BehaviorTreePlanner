using UnityEngine;

namespace BehaviorTreePlanner.Nodes
{
    public class BlackBoard : StaticNode
    {
        public override void InitializeNode(NodeDesign nd)
        {
            base.InitializeNode(nd);
        }

        public override void InitializeLoad(SavedNodeBase savedata)
        {
            base.InitializeLoad(savedata);
        }

        public override void Load()
        {
            base.Load();
        }

        public override SavedNodeBase Save()
        {
            return new SavedStaticNode
                (
                    -1,
                    lineHandler.AttachedPoint != null ? lineHandler.AttachedPoint.SaveIndex : -1,
                    lineHandler.SpawnedPoint != null ? lineHandler.SpawnedPoint.SaveIndex : -1
                );
        }
    }
}