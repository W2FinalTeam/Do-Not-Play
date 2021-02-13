using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class Door : ChangeableItem
{
    public GameObject[] unlockItem;//解锁道具
    private bool isOpen;
    private bool isUnLock;
    private GameObject player;
    Player p;
    private Animator anim;
    public BoxCollider boxCollider;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        p = player.GetComponent<Player>();
        isOpen = false;
        isUnLock = false;
        anim = GetComponentInChildren<Animator>();
    }

    public override void Interact(GameObject inHandItem)
    {
        if (!isUnLock)
        {
            UnLock();
        }
        if (isUnLock)
        {
            anim.SetBool("isDoorOpen", !isOpen);
            isOpen = !isOpen;
        }
    }
    public override void Interact()
    {
    }
    private void UnLock()
    {
        if (unlockItem == null)
        {
            isUnLock = true;
            return;
        }
        foreach (GameObject gameObject in unlockItem)
        {
            if (!p.ItemList.Contains(gameObject.name))
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
            isOpen = true;
            anim.SetBool("isDoorOpen", true);
            boxCollider.isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mother"))
        {
            isOpen = false;
            anim.SetBool("isDoorOpen", false);
            boxCollider.isTrigger = false;
        }
    }
}