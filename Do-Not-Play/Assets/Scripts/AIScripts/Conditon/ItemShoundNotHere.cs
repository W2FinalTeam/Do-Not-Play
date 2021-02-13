using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
//新增加，需同步
public class ItemShoundNotHere : Conditional
{
    private Mother mother;
    public override void OnAwake()
    {
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        if (mother.FindItem == null ||
            mother.FindItem.GetComponent<KeyItem>().IsChecked2 == true)
            return TaskStatus.Failure;
        return TaskStatus.Success;
    }
}
