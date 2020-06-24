using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Car : Tool
{
    GameManager GameManager;
    GameObject player;
    GameObject Camera_main;
    GameObject Camera_Car;

    public override GameObject PickUpItem(Transform parent)
    {
        //将车置于角色看不见的地方
        this.transform.position = new Vector3(100, 0, 0);
        return this.gameObject;
    }

    /// <summary>
    /// 将视野从车转为人物关闭CarController
    /// </summary>
    public override void UnUse()
    {
        Camera_Car.SetActive(false);
        Camera_main.SetActive(true);
        player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<Player>().enabled = true;
        this.gameObject.GetComponent<CarController>().enabled = false;
    }

    /// <summary>
    /// 将视野从人物转为车开启CarController
    /// </summary>
    public override void Use()
    {
        this.transform.position = new Vector3(player.transform.position.x + 1, 0, player.transform.position.z);
        this.transform.rotation = player.transform.rotation;
        Camera_Car.SetActive(true);
        Camera_main.SetActive(false);
        player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<Player>().enabled = false;
        this.gameObject.GetComponent<CarController>().enabled = true;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Camera_Car = GameObject.Find("遥控汽车/Camera_Car");
        Camera_main = GameObject.Find("Player/Camera_Main");
    }
    private void Start()
    {
        
    }
    /// <summary>
    /// 按G停止控制小车
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            UnUse();
        }

    }
}
