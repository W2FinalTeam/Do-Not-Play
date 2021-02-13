using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class Mobile : Tool
{

    GameManager GameManager;
    Timer timer;
    //摄像头组
    public GameObject cameraGroup;
    public RenderTexture renderTexture;
    private Camera[] cameras;
    int currentCamera;

    Player player;

    public override GameObject PickUpItem(Transform parent)
    {
        timer.enabled = true;
        Destroy(this.gameObject.GetComponent<Rigidbody>());
        this.transform.position = new Vector3(0,0,0);
        return this.gameObject;
    }
   
    public override void UnUse()
    {
        isusing = !isusing;
        GameManager.UIManager.SetUI(transform.name+"p", false);

        int level = GameManager.LevelManager.level;
        level = level >= 2 ? 1 : level;
        cameraGroup = GameObject.Find("AllCameraGroup/Level" + level);

        cameras = cameraGroup.GetComponentsInChildren<Camera>(true);

        cameras[currentCamera].enabled = false;
        player.FPC.enabled = true;
        player.isUsing = false;
        player.enabled = true;
    }

    public override void Use()
    {
        isusing = !isusing;
        int level = GameManager.LevelManager.level;
        level = level >= 2 ? 1 : level;
        cameraGroup = GameObject.Find("AllCameraGroup/Level" + level);
        cameras = cameraGroup.GetComponentsInChildren<Camera>(true);
        currentCamera = 0;

        GameManager.UIManager.SetUI(transform.name+"p", true);
        cameras[currentCamera].enabled = true;
        cameras[currentCamera].targetTexture = renderTexture;

        player.FPC.enabled = false;
        player.isUsing = true;
        player.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Start()
    {
        task = "获得手机";
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        GameManager.UIManager.UImain["手机p"].UI.transform.Find("Button").GetComponent<Button>().onClick.AddListener(
            delegate ()
            {
                UnUse();
            });
        timer = GetComponent<Timer>();

        player = GameManager.p;
    }

    // Update is called once per frame
    void Update()
    {
        if (isusing)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                cameras[currentCamera].targetTexture = null;
                cameras[currentCamera].enabled = false;
                currentCamera = currentCamera == (cameras.Length - 1) ? 0 : currentCamera + 1;
                cameras[currentCamera].enabled = true;
                cameras[currentCamera].targetTexture = renderTexture;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                cameras[currentCamera].targetTexture = null;
                cameras[currentCamera].enabled = false;
                currentCamera = currentCamera == 0 ? cameras.Length - 1 : currentCamera - 1;
                cameras[currentCamera].enabled = true;
                cameras[currentCamera].targetTexture = renderTexture;
            }
        }
    }
}
