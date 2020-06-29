using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class MoveableItem : BaseItem, IMoveableItem
{
    private bool inHand;
    private GameObject role;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Init()
    {
        myTransform = this.transform;
        inHand = false;
        role = null;
    }
    public override void Destory()
    {
        Destroy(gameObject);
    }

    public abstract void PlayDropSound();
    public abstract void ThrowItem(bool inHand);
    public abstract void PickUpItem(GameObject role);
    public abstract void ThrowItem();
    public abstract void PickUpItem();
}
