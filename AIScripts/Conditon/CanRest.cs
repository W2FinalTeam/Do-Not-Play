using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanRest : Conditional 
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("紧张度降到一定程度就休息")]
    public float maxTension;
    public bool isRunning = false;
    private Mother mother;
    public override void OnStart()
    {
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        if (mother.WholeTension < maxTension)
        {
            return TaskStatus.Success;
        }
        isRunning = false;
        return TaskStatus.Failure;
    }
}
