using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner;
//由于坐标判断不稳定，决定写一个脚本KeyItem 上面拥有判断是否被拿走的变量
public class ItemCheckTrigger : MonoBehaviour
{
    public GameObject ItemShouldHere;
    private void Start()
    {
        ItemShouldHere = transform.parent.gameObject;
        transform.parent = null;
    }
    private void OnTriggerExit(Collider role)
    {
        if (role.gameObject.CompareTag("Mother"))
            role.GetComponent<Mother>().lostItem = null;
    }
    private void OnTriggerEnter(Collider role)
    {
        if (role.gameObject.CompareTag("Mother"))
        {
            if (ItemShouldHere.GetComponent<KeyItem>().CheckItemTaken())
            {
                role.GetComponent<Mother>().lostItem = ItemShouldHere;
            }
        }
    }
}


