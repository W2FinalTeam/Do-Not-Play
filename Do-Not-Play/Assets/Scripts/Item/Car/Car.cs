using BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Car : Tool
{
    GameManager gameManager;
    Camera Camera_main;
    Camera Camera_Car;
    public override GameObject PickUpItem(Transform parent)
    {
        gameManager.LevelManager.CheckTask(task);
        //将车置于角色看不见的地方
        this.transform.position = new Vector3(100, 0, 0);
        return this.gameObject;
    }

    /// <summary>
    /// 将视野从车转为人物关闭CarController
    /// </summary>
    public override void UnUse()
    {
        Camera_Car.enabled=false;
        Camera_main.enabled = true;
        gameManager.p.FPC.m_MouseLook.lockCursor = true;
        gameManager.p.FPC.enabled = true;
        gameManager.p.isUsing = false;
        gameManager.p.enabled = true;
        this.gameObject.GetComponent<CarController>().enabled = false;
        isusing = false;
    }

    /// <summary>
    /// 将视野从人物转为车开启CarController
    /// </summary>
    public override void Use()
    {
        transform.SetParent(null);
        this.transform.position = new Vector3(gameManager.p.transform.position.x + 1, gameManager.p.transform.position.y+0.2f, gameManager.p.transform.position.z);
        this.transform.rotation = gameManager.p.transform.rotation;
        Camera_Car.enabled = true;
        Camera_main.enabled = false;
        gameManager.p.FPC.m_MouseLook.lockCursor = false;
        gameManager.p.FPC.enabled = false;
        gameManager.p.GetComponent<Player>().isUsing = true;
        gameManager.p.GetComponent<Player>().enabled = false;
        gameManager.UIManager.SetUI("Tab",false);
        this.gameObject.GetComponent<CarController>().enabled = true;
    }

    private void Awake()
    {
        task = "获得遥控汽车";

        Camera_Car = transform.Find("Camera_Car").gameObject.GetComponent<Camera>();
        Camera_main = GameObject.Find("Player/Camera_Main").GetComponent<Camera>();
        Camera_Car.GetComponent<Camera>().enabled = false; 
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    /// <summary>
    /// 按G停止控制小车
    /// </summary>
}
