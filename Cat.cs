using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : ChangeableItem
{
    public GameObject apple;
    private bool isTrigger=false;
    private AudioSource clip;
    // Start is called before the first frame update
    void Start()
    {
        clip =gameObject.GetComponent<AudioSource>();
        }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interact()
    {
       if(isTrigger)
        {
            //停止播放猫叫
            clip.enabled = false;
            
        }
       else
        {
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //开门以后，且旁边没有水果，开始播放猫叫
        if(!isTrigger)
        {
            clip.enabled = true;
            clip.Play();//循环播放
        }
        else
        {
            return;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject==apple)
        {
            isTrigger = true; //苹果扔到猫旁边
           
           

        }
    }
}
