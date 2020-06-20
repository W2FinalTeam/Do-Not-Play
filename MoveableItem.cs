using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : BaseItem, IMoveableItem
{
    private Transform HandLocation;
    private bool inHand;
    private GameObject role;
    public GameObject rightHandPositon;
    AudioSource audio;
    private Transform m;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        m = gameObject.GetComponent<Transform>();
       
        ThrowItem(true);
        Invoke("PlayDropSound", 1f);
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
    /// <summary>
    /// 摧毁道具
    /// </summary>
    protected override void Destory()
    {
        Destroy(this.gameObject);
    }
    public void Interact(GameObject target)
    {
        role = target;
        inHand = true;
        PickUpItem(role);
    }

    public void PlayDropSound() //播放物品掉落的声音
    {
       audio.PlayOneShot((AudioClip)Resources.Load("物品掉落"));
    }

    /// <summary>
    /// 按下G键向人物前方投掷手中物品
    /// </summary>
    /// <param name="InHand">手中是否有物品</param>
    public void ThrowItem(bool InHand) 
    {
        if(InHand)
        {
            //首先判断是否为子物体
            if (m.parent == this.transform)
           
            {
                //让物品接受物理事件
                this.GetComponent<Rigidbody>().isKinematic = false;
                //运用TransformDirection()方法获取一个方向
                Vector3 camDirct = m.TransformDirection(0, 3, 3);
                //为物品添加一个向前的冲量
                this.GetComponent<Rigidbody>().AddForce(camDirct, ForceMode.Impulse);
            }
            else
            {
                Debug.Log(m.position);
                return;
            }
        }
        
    }
    //负责实现-拾取物品将物品展示在手上
    public void PickUpItem(GameObject role)
    {
        if (role.tag == "Child")
        {
            this.gameObject.transform.position = rightHandPositon.transform.position;
            this.transform.parent = role.transform;
        }

        if (role.tag == "Mother" || role.tag == "Father")
        {
            //--------
        }

    }
  

    /// <summary>
    /// 当玩家靠近物品时显示按键UI
    /// </summary>
    public void ShowInfo()
    {

    }

}
