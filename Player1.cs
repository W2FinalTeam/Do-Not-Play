using Boo.Lang;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public GameManager GameManager;
    //可拾取距离
    public float reachRange;
    //存储永久性获得道具
    public List<GameObject> ItemList= new List<GameObject>();
    //在手中的物品
    public GameObject inHandItem = null;
    //被射线检测到的物体
    public GameObject targetItem;
    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            Debug.DrawLine(ray.origin, hit.point);
            targetItem = hit.collider.gameObject;
        }
        else
            targetItem = null;
    }
    private void KeyEvent()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetItem.tag == "MoveableItem" && inHandItem == null)
            {
                targetItem.GetComponent<MoveableItem>().PickUpItem();
                inHandItem = targetItem;
                return;
            }
            if (targetItem.tag == "Tool")
            {
                ItemList.Add(targetItem.GetComponent<Tool>().PickUpItem(this.transform));
                GameManager.SetTool(targetItem.name, targetItem);
                
                return;
            }
            if (targetItem.tag == "ChangeableItem")
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

