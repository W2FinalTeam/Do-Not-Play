using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : BaseItem, IMoveableItem
{
    private Transform rightHandLocation;
    private AudioSource clip;
    //声音距离
    public float MySoundRange = 15f;
    public Mother mother;
    private bool isPlayed = false;

    Vector3 mid = new Vector3(0.5f, 0.5f, 0);
    // Start is called before the first frame update
    void Start()
    {
        clip = gameObject.GetComponent<AudioSource>();
        clip.enabled = false;
        mother = GameObject.FindWithTag("Mother").GetComponent<Mother>();
        rightHandLocation = GameObject.Find("RightHandPosition").transform;
        myTransform = this.transform;
    }

    public override void Init()
    {
        transform.position = myTransform.position;
        transform.rotation = myTransform.rotation;
    }
    public override void Destory()
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
    {
        gameObject.layer = 0;
        //解除父子关系
        this.transform.SetParent(null);
        //让物品接受物理事件
        this.GetComponent<Rigidbody>().isKinematic = false;
        //运用TransformDirection()方法获取一个方向
        Vector3 camDirct = Camera.main.ViewportPointToRay(mid).direction;
        //为物品添加一个向前的冲量
        this.GetComponent<Rigidbody>().AddForce(camDirct, ForceMode.Impulse);
        clip.enabled = true;
        isPlayed = false;
    }
    //负责实现-拾取物品将物品展示在手上
    public void PickUpItem(GameObject role)
    {
        if (role.CompareTag("Player"))
        {
            gameObject.layer = 9;
            this.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = rightHandLocation.position;
            transform.rotation = rightHandLocation.rotation;
            transform.SetParent(rightHandLocation);
        }

        else if (role.CompareTag("Mother") || role.CompareTag("Father"))
        {
            //--------
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (clip.enabled && isPlayed == false)
        {
            PlayDropSound();
            isPlayed = true;
            CheckSound();
        }
    }
    private void CheckSound()
    {
        if (Vector3.Distance(transform.position, mother.transform.position) < MySoundRange)
        {
            mother.HearSomething = true;
            mother.HearTarget = transform;
        }
        else
        {
            mother.HearSomething = false;
            mother.HearTarget = null;
        }
    }
}