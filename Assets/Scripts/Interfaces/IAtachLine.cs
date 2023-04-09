using BehaviorTreePlanner.Lines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreePlanner
{
    public interface IAtachLine
    {
         void AttachLine(LinePoint Line);
         void DeAttachLine();
    }
}