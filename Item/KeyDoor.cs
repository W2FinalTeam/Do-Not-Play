using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : ChangeableItem
{
    public GameObject axe;//斧头
   

    public override void Interact(GameObject inHandItem)
    {
        if (inHandItem == axe)
        {
            this.GetComponent<Animator>().SetBool("isOpen_Obj_1", true);
        }
    }
    public override void Interact()
    {

    }
}