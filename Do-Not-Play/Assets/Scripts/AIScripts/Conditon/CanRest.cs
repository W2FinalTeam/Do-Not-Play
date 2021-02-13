using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanRest : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("紧张度降到一定程度就休息")]
    public float maxTension;
    public SharedBool isRunning;
    private Mother mother;
    public override void OnStart()
    {
        isRunning.Value = false;
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        //进入条件：紧张度小于一定范围且isRunning==false，退出条件：isRunning==false
        if (mother.WholeTension <= maxTension && !isRunning.Value)
        {
            isRunning.Value = true;
            return TaskStatus.Success;
        }
        if (!isRunning.Value)
        {
            isRunning.Value = false;
            return TaskStatus.Failure;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}
