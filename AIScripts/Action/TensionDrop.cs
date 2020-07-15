using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class TensionDrop : Action 
{
    public int HowManyFramesPerDrop;
    public int DropValue;
    private int Frame=0;
    private Mother mother;
    public override void OnAwake()
    {
        mother = GetComponent<Mother>();
        Frame = 0;
    }
    public override TaskStatus OnUpdate()
    {
        if(++Frame% HowManyFramesPerDrop == 0)
            mother.ChangeTempTensionValue(DropValue);
        return TaskStatus.Success;
    }
}
