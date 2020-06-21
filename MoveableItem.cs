using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : BaseItem, IMoveableItem
{
    public Transform rightHandLocation;
    private bool inHand;
    private GameObject role;
    private AudioSource clip;
    // Start is called before the first frame update
    void Start()
    {
        clip = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void Init()
    {
        myTransform = this.transform;
        inHand = false;
        role = null;
    }
    protected override void Destory()
    {
        Destroy(this.gameObject);
    }
    public void PlayDropSound()
    {
        clip.PlayOneShot((AudioClip)Resources.Load("物品掉落"));
    }
    /// <summary>
    /// 按下G键向人物前方投掷手中物品
    /// </summary>
    public void ThrowItem()
    {//不再放在手上
        inHand = false;
        //解除父子关系
        this.transform.SetParent(null);
        //让物品接受物理事件
        this.GetComponent<Rigidbody>().isKinematic = false;
        //获取主角方向
        Vector3 camDirct = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        //为物品添加一个向前的冲量
        this.GetComponent<Rigidbody>().AddForce(camDirct, ForceMode.Impulse);
        Invoke("PlayDropSound", 0.8f);
    }
    //负责实现-拾取物品将物品展示在手上
    public void PickUpItem(GameObject role)
    {
        this.role = role;
        inHand = true;
        if (role.CompareTag("Child"))
        {
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = rightHandLocation.position;
            transform.rotation = rightHandLocation.rotation;
            transform.parent = role.transform;
        }
        else if (role.CompareTag("Mother") || role.CompareTag("Father"))
        {
            //--------
        }

    }
}