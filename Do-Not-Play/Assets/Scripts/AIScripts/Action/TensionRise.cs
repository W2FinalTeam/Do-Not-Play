using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class TensionRise : Action
{
    public int StaticRiseValue = 100;
    public int TempRiseValue = 100;
    private Mother mother;
    public override void OnStart()
    {
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        mother.ChangeStaticTensionValue(StaticRiseValue);
        mother.ChangeTempTensionValue(TempRiseValue);
        return TaskStatus.Success;
    }
}
