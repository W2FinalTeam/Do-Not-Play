﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : BaseItem, IMoveableItem
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
    protected override void Init()
    {
        myTransform = this.transform;
        inHand = false;
        role = null;
    }
    protected override void Destory()
    {
        Destroy(gameObject);
    }
}
