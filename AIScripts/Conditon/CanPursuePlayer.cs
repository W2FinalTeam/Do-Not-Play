using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class CanPursuePlayer : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("紧张度阈值")]
    public float minTension;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("限定时间")]
    public Time limitTime;
    private Mother mother;
    public override void OnStart()
    {
        mother=GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        if (mother.WholeTension >= minTension)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
