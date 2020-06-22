using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class Mobile : Tool
{
    public GameObject phoneui;
    GameManager GameManager;
    GameObject player;
    /// <summary>
    /// 物体对应任务
    /// </summary>
    string task;

    public override GameObject PickUpItem(Transform parent)
    {
        if (GameManager.HaveTask(task) && !GameManager.GetTask(task)) 
        {
            GameManager.SetTask(task, true);
        }
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        this.transform.position = new Vector3(0,0,0);
        player = GameObject.FindGameObjectWithTag("Player");
        return this.gameObject;
    }
   
    public override void UnUse()
    {
        isusing = !isusing;
   //     player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        this.phoneui.SetActive(false);
 //       Cursor.visible = true;
   //     Debug.Log("unuse");
    }

    public override void Use()
    {
        isusing = !isusing;
        player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;//鼠标锁定与解锁
        this.phoneui.SetActive(true);
        Cursor.visible = true;
        Debug.Log("use");
    }

    // Start is called before the first frame update
    void Start()
    {
        task = "获得手机";
        this.phoneui.transform.Find("Button").GetComponent<Button>().onClick.AddListener(
            delegate ()
            {
                UnUse();
            });
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
