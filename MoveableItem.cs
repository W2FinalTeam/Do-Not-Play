using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : BaseItem, IMoveableItem
{
    private Transform rightHandLocation;
    private bool inHand;
    private GameObject role;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {

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
        audio.PlayOneShot((AudioClip)Resources.Load("物品掉落"));
    }
    /// <summary>
    /// 按下G键向人物前方投掷手中物品
    /// </summary>
    public void ThrowItem()
    {
        if (inHand)
        {
            transform.parent = null;
            //让物品接受物理事件
            this.GetComponent<Rigidbody>().isKinematic = false;
            //运用TransformDirection()方法获取一个方向
            Vector3 camDirct = transform.TransformDirection(0, 3, 3);
            //为物品添加一个向前的冲量
            this.GetComponent<Rigidbody>().AddForce(camDirct, ForceMode.Impulse);
        }
    }
    //负责实现-拾取物品将物品展示在手上
    public void PickUpItem(GameObject role)
    {
        this.role = role;
        inHand = true;
        if (role.CompareTag("Child"))
        {
            transform.position = rightHandLocation.position;
            transform.parent = role.transform;
        }

        if (role.CompareTag("Mother") || role.CompareTag("Father"))
        {
            //--------
        }

    }
}