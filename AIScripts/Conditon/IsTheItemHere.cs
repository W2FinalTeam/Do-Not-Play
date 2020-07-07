﻿using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class IsTheItemHere : Conditional 
{
    public SharedGameObject Mother;
    public override void OnAwake()
    {
        Mother = GameObject.FindWithTag("Mother");
   }
    public override TaskStatus OnUpdate()
    {
        if (Mother.Value.GetComponent<Mother>().lostItem == null || Mother.Value.GetComponent<Mother>().lostItem.GetComponent<KeyItem>().IsChecked == true)
            return TaskStatus.Failure;
        return TaskStatus.Success;
    }
}
