using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : BaseItem, IMoveableItem
{
    private Transform HandLocation;
    private bool inHand;
    private GameObject role;
    public GameObject rightHandPositon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Init()
    {
        myTransform = this.transform;
        inHand = false;
        role = null;
    }
    protected override void Destory()
    {
        Destroy(this.gameObject);
    }
    protected override void ShowInfo()
    {
    }
    public void Interact(GameObject target)
    {
        role = target;
        inHand = true;
        PickUpItem(role);
    }

    public void PlayDropSound()
    {
            
    }

    public void ThrowItem(bool inHand)
    {
        
    }
    //负责实现-拾取物品将物品展示在手上
    public void PickUpItem(GameObject role)
    {
        if (role.tag == "Child")
        {
            this.gameObject.transform.position = rightHandPositon.transform.position;
            this.transform.parent = role.transform;
        }

        if (role.tag == "Mother" || role.tag == "Father")
        {
            //--------
        }

    }
}
