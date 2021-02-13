using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
//被改变，需同步
public class Player : MonoBehaviour
{    //房间出生点
    public Transform room;

    public GameManager gameManager;
    //可拾取距离
    public float reachRange = 0.5f;
    //存储永久性获得道具
    public List<string> ItemList = new List<string>();
    //在手中的物品
    public GameObject inHandItem = null;
    //被射线检测到的物体
    public GameObject targetItem;
    public FirstPersonController FPC;

    Vector3 mid = new Vector3(0.5f, 0.5f, 0);

    public bool isUsing = false;
    private void Start()
    {
        FPC = GetComponent<FirstPersonController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Load();
    }
    private void Update()
    {
        AxisAnalysis();
        KeyEvent();
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G) && inHandItem != null)
        {
            inHandItem.GetComponent<MoveableItem>().ThrowItem();
            if (inHandItem.GetComponent<KeyItem>() != null)
            {
                inHandItem.GetComponent<KeyItem>().PlayerThrowMe();
                ItemList.Remove(inHandItem.name);
            }
            inHandItem = null;
        }
    }
    private void AxisAnalysis()
    {
        Ray ray = Camera.main.ViewportPointToRay(mid);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachRange, 1 << 0 | 1 << 2 | 0 << 9))
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
                ItemList.Add(targetItem.name);
                return;
            }
            if (targetItem.CompareTag("Tool"))
            {
                ItemList.Add(targetItem.GetComponent<Tool>().PickUpItem(this.transform).name);
                if (!gameManager.Tools.ContainsKey(targetItem.name))
                    gameManager.Tools.Add(targetItem.name, targetItem);
                else
                {
                    gameManager.Tools[targetItem.name] = targetItem;
                }
                return;
            }
            if (targetItem.CompareTag("ChangeableItem"))
            {
                targetItem.GetComponent<ChangeableItem>().Interact(inHandItem);
                return;
            }

        }
        if (inHandItem != null && gameManager.UIManager.UImain["KeyTipE"].isShow == false)
        {
            gameManager.UIManager.SetUI("KeyTipG", true);

        }
        else
            gameManager.UIManager.SetUI("KeyTipG", false);
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
    }
    /// <summary>
    /// 被母亲抓到后重新开始
    /// </summary>
    public void Restart()
    {
        if (inHandItem != null)
        {
            inHandItem.GetComponent<MoveableItem>().ThrowItem();
            inHandItem.GetComponent<MoveableItem>().Init();
            inHandItem = null;
        }
        this.transform.position = room.position;
        gameManager.Restart();
    }
    public void Load()
    {
        UIManager.GetInstance().SetUI("菜单", false);
        StartCoroutine(GMLoad());
    }
    IEnumerator GMLoad()
    {
        FPC.enabled = false;
        gameManager.Load();
        yield return new WaitForSeconds(2f);
        FPC.enabled = true;
        FPC.m_MouseLook.lockCursor = true;
        Cursor.visible = false;
    }
}