using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : ChangeableItem
{
    public GameObject key;
    private bool isOpen;
    private bool isUnLock;
    void Start()
    {
        Init();
        isOpen = false;
        isUnLock = false;
    }


    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact()
    {
        if (!isUnLock)
        {
            UnLock();
        }
        if (isUnLock)
        {
            Vector3 force = transform.forward * 1000f * (isOpen == true ? 1 : -1);
            this.GetComponent<Rigidbody>().AddForce(force, ForceMode.Acceleration);
            isOpen = !isOpen;
        }
    }
    private void UnLock()
    {
        foreach (GameObject temp in Player.ItemList)
        {
            if (temp == key)
            {
                isUnLock = true;
                return;
            }
        }
    }
}
