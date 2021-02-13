using BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMeetTrigger : MonoBehaviour
{
    public GameObject ItemBeMet;
    private void Start()
    {
        ItemBeMet = transform.parent.gameObject;
    }
    private void OnTriggerEnter(Collider role)
    {
        if(role.gameObject.CompareTag("Mother"))
        {
            if(ItemBeMet.GetComponent<KeyItem>().CheckItemThrown())
            {
                role.GetComponent<Mother>().FindItem = ItemBeMet;
            }
        }
    }
}
