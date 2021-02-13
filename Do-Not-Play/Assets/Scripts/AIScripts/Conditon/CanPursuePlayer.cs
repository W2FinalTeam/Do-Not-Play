using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class CanPursuePlayer : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("紧张度阈值")]
    public float minTension;
    private Mother mother;
    public override void OnStart()
    {
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        if (mother.WholeTension >= minTension || mother.TimeOver)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
