using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner;
//由于坐标判断不稳定，决定写一个脚本KeyItem 上面拥有判断是否被拿走的变量
public class ItemCheckTrigger : MonoBehaviour
{
    public  GameObject ItemShouldHere;
    public GameObject Mother;   
    private void Start()
    {
    }
    private void Update()
    {
       // CheckItem();
    }

    private void OnTriggerExit(Collider role)
    {
        if (role.gameObject.tag == "Mother")
            role.GetComponent<Mother>().lostItem = null;
    }
    private void OnTriggerEnter(Collider role)
    {
            if(role.gameObject.tag=="Mother")
            { 
                Debug.Log("motherin");
                if (ItemShouldHere.GetComponent<KeyItem>().CheckItemTaken())
                {
                    role.GetComponent<Mother>().lostItem = ItemShouldHere;
                }          
            }
    }
}


