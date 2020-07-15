using Boo.Lang;
using System;
using UnityEngine;
//被改变，需同步
public class Player : MonoBehaviour
{    //房间出生点
    public Transform room;

    public GameManager gameManager;
    //可拾取距离
    public float reachRange;
    //存储永久性获得道具
    public List<GameObject> ItemList = new List<GameObject>();
    //在手中的物品
    public GameObject inHandItem = null;
    //被射线检测到的物体
    public GameObject targetItem;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    private void Update()
    {
        AxisAnalysis();
        KeyEvent();
    }
    private void AxisAnalysis()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachRange))
        {
            targetItem = hit.collider.gameObject;
            if (targetItem.GetComponent<BaseItem>() != null)
            {
                gameManager.UIManager.SetUI("KeyTipE", true);
            }
            else
                gameManager.UIManager.SetUI("KeyTipE", false);
        }
        else
        {
            targetItem = null;
            gameManager.UIManager.SetUI("KeyTipE", false);
        }
    }
    private void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameManager.UIManager.SetUI("Tab", !gameManager.UIManager.UImain["Tab"].isShow);
            gameManager.ShowInTab();
        }
        if (targetItem == null)
            return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetItem.GetComponent<KeyItem>() != null)
            {
                targetItem.GetComponent<KeyItem>().PlayerTookMe();
            }
            if (targetItem.CompareTag("MoveableItem") && inHandItem == null)
            {
                targetItem.GetComponent<MoveableItem>().PickUpItem(this.gameObject);
                inHandItem = targetItem;
                ItemList.Add(targetItem);
                return;
            }
            if (targetItem.CompareTag("Tool"))
            {
                ItemList.Add(targetItem.GetComponent<Tool>().PickUpItem(this.transform));
                gameManager.SetTool(targetItem.name, targetItem);
                return;
            }
            if (targetItem.CompareTag("ChangeableItem"))
            {
                targetItem.GetComponent<ChangeableItem>().Interact(inHandItem);
                return;
            }

        }
        if (inHandItem != null)
        {
            gameManager.UIManager.SetUI("KeyTipG", true);

        }
        else
            gameManager.UIManager.SetUI("KeyTipG", false);
        if (Input.GetKeyDown(KeyCode.G) && inHandItem != null)
        {
            inHandItem.GetComponent<MoveableItem>().ThrowItem();
            inHandItem = null;
            if (targetItem.GetComponent<KeyItem>() != null)
            {
                targetItem.GetComponent<KeyItem>().PlayerThrowMe();
            }
        }
    }
    /// <summary>
    /// 被母亲抓到后重新开始
    /// </summary>
    public void Restart()
    {
        if (inHandItem != null)
        {
            inHandItem.GetComponent<MoveableItem>().ThrowItem();
            inHandItem = null;
        }
        this.transform.position = room.position;
        gameManager.Restart();
    }
}