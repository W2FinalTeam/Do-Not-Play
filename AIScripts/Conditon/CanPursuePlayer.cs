using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class CanPursuePlayer : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("紧张度")]
    public SharedFloat tension;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("紧张度阈值")]
    public float minTension;
    public override TaskStatus OnUpdate()
    {
        if (tension.Value >= minTension)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
