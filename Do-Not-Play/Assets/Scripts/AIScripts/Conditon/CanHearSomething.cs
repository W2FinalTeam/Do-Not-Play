using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class CanHearSomething : Conditional
{
    private Mother mother;
    public override void OnStart()
    {
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        if (mother.IsCanHear())
        {
            mother.Destination = mother.HearTarget.position;
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Failure;
    }
}
