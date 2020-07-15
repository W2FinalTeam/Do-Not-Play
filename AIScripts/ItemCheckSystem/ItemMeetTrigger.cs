using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMeetTrigger : MonoBehaviour
{
    public GameObject ItemBeMet;
    public GameObject mother;
    private void OnTriggerEnter(Collider role)
    {
        if(role.gameObject.CompareTag("Mother"))
        {
            Debug.Log("MotherMeetTheItem");
            if(ItemBeMet.GetComponent<KeyItem>().CheckItemThrown())
            {
                role.GetComponent<Mother>().FindItem = ItemBeMet;
            }
        }
    }
}
