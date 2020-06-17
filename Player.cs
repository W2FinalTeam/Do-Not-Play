using Boo.Lang;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    //可拾取距离
    public float reachRange;
    //存储永久性获得道具
    public List<Tool> ItemList;
    //在手中的物品
    public GameObject inHandItem = null;

    private void Start()
    {

    }
    private void Update()
    {
        KeyEvent();
    }
    public void AxisAnalysis()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachRange))
        {
            if (hit.transform.tag == "MoveableItem")
            {
                hit.transform.GetComponent<MoveableItem>().Interact(this.gameObject);
                inHandItem = hit.collider.gameObject;
                return;
            }
            if (hit.transform.tag == "Tool")
            {
                ItemList.Add(hit.transform.GetComponent<Tool>().PickUpItem());
                return;
            }
            if (hit.transform.tag == "ChangeableItem")
            {
                hit.transform.GetComponent<ChangeableItem>().Interact();
                return;
            }
        }
    }
    private void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.E))
            AxisAnalysis();
        if (Input.GetKeyDown(KeyCode.G) && inHandItem != null)
        {
            inHandItem.GetComponent<MoveableItem>().ThrowItem();
            inHandItem = null;
        }
    }
}

