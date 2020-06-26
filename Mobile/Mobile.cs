using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class Mobile : Tool
{
    public GameObject phoneui;
    //摄像头组
    public GameObject cameraGroup;
    public RenderTexture renderTexture;
    private Camera[] cameras;
    int currentCamera;

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
        this.phoneui.SetActive(false);

        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<Player>().enabled = true;
    }

    public override void Use()
    {
        isusing = !isusing;
        this.phoneui.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<Player>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
            if (Input.GetKeyDown(KeyCode.RightArrow)){
                cameras[currentCamera].targetTexture = null;
                currentCamera = currentCamera == (cameras.Length - 1) ? 0 : currentCamera +1;
                cameras[currentCamera].targetTexture = renderTexture;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                cameras[currentCamera].targetTexture = null;
                currentCamera = currentCamera  == 0 ? cameras.Length-1 : currentCamera - 1;
                cameras[currentCamera].targetTexture = renderTexture;
            }
        }
    }
}
