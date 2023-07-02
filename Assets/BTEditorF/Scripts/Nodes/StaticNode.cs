using BehaviorTreePlanner.Lines;
using BehaviorTreePlanner.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public class StaticNode : NodeBase
    {
        public override SavedNodeBase Save()
        {
            throw new System.NotImplementedException();
        }
        public override void InitializeNode(NodeDesign nd, EditorManager editormanager)
        {
            base.InitializeNode(nd, editormanager);
            lineHandler.InitializeLineHandler(null, null, LineTrigger.transform, editorManager);
        }

        public override void InitializeLoad(SavedNodeBase savedata, EditorManager editormanager)
        {
            base.InitializeLoad(savedata, editormanager);
        }
        public override void AttachLine(LinePoint Line)
        {
        }

        public override void DeAttachLine()
        {
        }

        public override void Load()
        {
            base.Load();
        }

    }
}
