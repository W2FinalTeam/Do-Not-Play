using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class FeelingAbilityRise : Action
{
    public SharedFloat hear;
    public SharedFloat sight;

    public override TaskStatus OnUpdate()
    {
        Debug.Log("RISE...........................");
        hear.Value += 1;
        sight.Value += 1;
        return TaskStatus.Success;
    }

}
