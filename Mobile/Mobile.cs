using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class Mobile : Tool
{

    GameManager GameManager;

    //摄像头组
    public GameObject cameraGroup;
    public RenderTexture renderTexture;
    private Camera[] cameras;
    int currentCamera;

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

        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<Player>().enabled = true;
    }

    public override void Use()
    {
        isusing = !isusing;

        GameManager.UIManager.SetUI(transform.name, true);

        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<Player>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

       override public void Init()
    {
        task = "获得手机";
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        GameManager.UIManager.UImain["手机"].UI.transform.Find("Button").GetComponent<Button>().onClick.AddListener(
            delegate ()
            {
                UnUse();
            });
       
   
    }
    void Start()
    {
        cameraGroup = GameObject.Find("CameraGroup");
        cameras = cameraGroup.GetComponentsInChildren<Camera>();
        currentCamera = 0;
        cameras[currentCamera].targetTexture = renderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (isusing)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                cameras[currentCamera].targetTexture = null;
                currentCamera = currentCamera == (cameras.Length - 1) ? 0 : currentCamera + 1;
                cameras[currentCamera].targetTexture = renderTexture;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                cameras[currentCamera].targetTexture = null;
                currentCamera = currentCamera == 0 ? cameras.Length - 1 : currentCamera - 1;
                cameras[currentCamera].targetTexture = renderTexture;
            }
        }
    }
}
