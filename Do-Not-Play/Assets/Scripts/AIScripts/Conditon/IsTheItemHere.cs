using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class IsTheItemHere : Conditional 
{
    private Mother mother;
    public override void OnAwake()
    {
        mother = GetComponent<Mother>();
    }

    public override TaskStatus OnUpdate()
    {
        if (mother.lostItem == null || mother.lostItem.GetComponent<KeyItem>().IsChecked == true)
            return TaskStatus.Failure;
        return TaskStatus.Success;
    }
}
