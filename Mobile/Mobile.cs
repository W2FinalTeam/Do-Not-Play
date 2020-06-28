using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class Mobile : Tool
{
  
    GameManager GameManager;
   
    
    GameObject player;
    /// <summary>
    /// 物体对应任务
    /// </summary>
    string task;

    public override GameObject PickUpItem(Transform parent)
    {
        if (GameManager.TaskManager.HaveTask(task) && !GameManager.TaskManager.GetTask(task)) 
        {
            GameManager.TaskManager.SetTask(task, true);
        }
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        this.transform.position = new Vector3(0,0,0);
        player = GameObject.FindGameObjectWithTag("Player");
        return this.gameObject;
    }
   
    public override void UnUse()
    {
        isusing = !isusing;
        GameManager.UIManager.SetUI(transform.name, false);
    }

    public override void Use()
    {
        isusing = !isusing;
        GameManager.UIManager.SetUI(transform.name, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        task = "获得手机";
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        GameManager.UIManager.UImain[transform.name].UI.transform.Find("Button").GetComponent<Button>().onClick.AddListener(
            delegate ()
            {
                UnUse();
            });
       
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
