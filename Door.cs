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
    public override void Init()
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
    public override void Interact(GameObject inHandItem)
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
    public override void Interact()
    {

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
            joint.targetVelocity = 100;
            gameObject.GetComponent<HingeJoint>().motor = joint;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mother"))
        {
            joint.targetVelocity = -100;
            gameObject.GetComponent<HingeJoint>().motor = joint;
        }
    }
}