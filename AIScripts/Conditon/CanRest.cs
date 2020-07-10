using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanRest : Conditional 
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("紧张度降到一定程度就休息")]
    public float maxTension;
    public override TaskStatus OnUpdate()
    {
        if (GetComponent<Mother>().WholeTension < maxTension)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
