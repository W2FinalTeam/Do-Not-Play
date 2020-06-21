using Boo.Lang;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    //可拾取距离
    public float reachRange;
    //存储永久性获得道具
    static public List<GameObject> ItemList;
    //在手中的物品
    public GameObject inHandItem = null;
    //被射线检测到的物体
    public GameObject targetItem;
    private void Start()
    {
    }
    private void Update()
    {
        AxisAnalysis();
        KeyEvent();
    }
    public void AxisAnalysis()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachRange))
        {
            targetItem = hit.collider.gameObject;
        }
        else
            targetItem = null;
    }
    private void KeyEvent()
    {
        if (targetItem == null)
            return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetItem.CompareTag("MoveableItem") && inHandItem == null)
            {
                targetItem.GetComponent<MoveableItem>().PickUpItem(this.gameObject);
                inHandItem = targetItem;
                return;
            }
            if (targetItem.CompareTag("Tool"))
            {
                targetItem.GetComponent<Tool>().PickUpItem();
                ItemList.Add(targetItem);
                return;
            }
            if (targetItem.CompareTag("ChangeableItem"))
            {
                targetItem.GetComponent<ChangeableItem>().Interact();
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && inHandItem != null)
        {
            inHandItem.GetComponent<MoveableItem>().ThrowItem();
            inHandItem = null;
        }
    }
}

