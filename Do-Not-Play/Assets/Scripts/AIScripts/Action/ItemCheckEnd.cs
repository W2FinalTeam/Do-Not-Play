using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
public class ItemCheckEnd : Action
{
    private Mother mother;
    public override void OnAwake()
    {
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        mother.lostItem.GetComponent<KeyItem>().IsChecked = true;
        mother.lostItem = null;
        return TaskStatus.Success;
    }
}
