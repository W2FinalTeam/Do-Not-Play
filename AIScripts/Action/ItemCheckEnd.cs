using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
public class ItemCheckEnd : Action
{
    public SharedGameObject Mother;
    public override void OnAwake()
    {
        Mother = GameObject.FindWithTag("Mother");
    }
    public override TaskStatus OnUpdate()
    {
        Mother.Value.GetComponent<Mother>().lostItem.GetComponent<KeyItem>().IsChecked = true;
        Mother.Value.GetComponent<Mother>().lostItem = null;
        return TaskStatus.Success;
    }
}
