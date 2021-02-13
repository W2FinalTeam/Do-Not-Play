using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : ChangeableItem
{
    public GameObject text;
    public override void Interact()
    {
       
    }
    public override void Interact(GameObject inHandItem)
    {
        text.SetActive(true);
    }
}
