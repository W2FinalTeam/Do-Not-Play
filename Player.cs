using Boo.Lang;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager GameManager;
    //可拾取距离
    public float reachRange;
    //存储永久性获得道具
    public List<GameObject> ItemList = new List<GameObject>();
    //在手中的物品
    public GameObject inHandItem = null;
    //被射线检测到的物体
    public GameObject targetItem;
    //GUI
    private bool showInteractMsg;
    private GUIStyle guiStyle;
    private string msg;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        setupGui();
      
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
                showInteractMsg = true;
            }
            else
                showInteractMsg = false;
        }
        else
        {
            targetItem = null;
            showInteractMsg = false;
        }
    }
    private void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            GameManager.UIManager.SetUI("Tab", !GameManager.UIManager.UImain["Tab"].isShow);
            GameManager.ShowInTab();
        }
        if (targetItem == null)
            return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(targetItem.GetComponent<KeyItem>()!=null)
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
                GameManager.SetTool(targetItem.name, targetItem);
                return;
            }
            if (targetItem.CompareTag("ChangeableItem"))
            {
                targetItem.GetComponent<ChangeableItem>().Interact(inHandItem);
                return;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.G) && inHandItem != null)
        {
            inHandItem.GetComponent<MoveableItem>().ThrowItem();
            inHandItem = null;
        }
    }
    #region GUI Config
    private void setupGui()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 16;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;
        msg = "Press E";
    }
    void OnGUI()
    {
        if (showInteractMsg)  //show on-screen prompts to user for guide.
        {
            GUI.Label(new Rect(50, Screen.height - 50, 200, 50), msg, guiStyle);
        }
    }
    //End of GUI Config --------------
    #endregion
}