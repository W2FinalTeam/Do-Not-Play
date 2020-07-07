using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class TensionRise : Action
{
    public SharedGameObject Mother;
    public int StaticRiseValue = 100;
    public int TempRiseValue = 100;

    public override TaskStatus OnUpdate()
    {
        Mother.Value.GetComponent<Mother>().ChangeStaticTensionValue(StaticRiseValue);
        Mother.Value.GetComponent<Mother>().ChangeTempTensionValue(TempRiseValue);
        return TaskStatus.Success;

    }
}
