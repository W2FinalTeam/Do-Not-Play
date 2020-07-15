using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : ChangeableItem
{
    public GameObject[] key;
    private bool isOpen;
    private bool isUnLock;
    private GameObject player;
    private Animator anim;
    void Start()
    {
        Init();
        player = GameObject.FindWithTag("Player");

    }
    public override void Init()
    {
        myTransform = this.transform;
        isOpen = false;
        isUnLock = false;
        anim = GetComponent<Animator>();
    }

    public override void Interact(GameObject inHandItem)
    {
        if (!isUnLock)
        {
            UnLock();
        }
        if (isUnLock)
        {
            isOpen = !isOpen;
            if (!isOpen)
            {
                anim.SetBool("isDoorOpen", true);
            }
            else
            {
                anim.SetBool("isDoorOpen", false);
            }
        }
    }
    public override void Interact()
    {
    }
    private void UnLock()
    {
        if (key == null)
        {
            isUnLock = true;
            return;
        }
        foreach (GameObject gameObject in key)
        {
            if (!player.GetComponent<Player>().ItemList.Contains(gameObject))
            {
                isUnLock = false;
                return;
            }
        }
        isUnLock = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mother"))
        {
            anim.SetBool("isDoorOpen", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mother"))
        {
            anim.SetBool("isDoorOpen", false);
        }
    }
}