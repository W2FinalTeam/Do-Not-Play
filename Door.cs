using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : ChangeableItem
{
    public GameObject[] key;
    private JointMotor joint;
    private bool isOpen;
    private bool isUnLock;
    private GameObject player;
    void Start()
    {
        Init();
        player = GameObject.FindWithTag("Player");
    }
    protected override void Init()
    {
        myTransform = this.transform;
        isOpen = false;
        isUnLock = false;

        joint.force = 20;
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
            joint.targetVelocity = isOpen == true ? -100 : 100;
            isOpen = !isOpen;

            gameObject.GetComponent<HingeJoint>().motor = joint;

        }

    }
    private void UnLock()
    {
        if (key == null )
        {
            isUnLock = true;
            return;
        }
        foreach (GameObject gameObject in key)
        {
            if (!player.ItemList.Equals(gameObject))
            {
                isUnLock = false;
                return;
            }
        }
        isUnLock = true;
    }
}