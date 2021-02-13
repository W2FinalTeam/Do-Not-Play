using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : ChangeableItem
{
    public GameObject apple;
    private bool isTrigger = false;
    private AudioSource clip;
    public Mother mother;
    public GameObject door;
    public float MySoundRange;
    public float MyCloseRange;

    // Start is called before the first frame update
    void Start()
    {
        clip = gameObject.GetComponent<AudioSource>();
        mother = GameObject.FindWithTag("Mother").GetComponent<Mother>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, mother.transform.position) < MyCloseRange)
        {
            isTrigger = true;
            clip.enabled = false;
            mother.HearSomething = false;
            mother.HearTarget = null;
        }
        if (clip.isPlaying)
        {
            if (Vector3.Distance(transform.position, mother.transform.position) < MySoundRange)
            {
                mother.HearSomething = true;
                mother.HearTarget = this.gameObject.transform;
            }

        }


    }
    public override void Interact(GameObject inHandItem)
    {
        if (isTrigger)
        {
            //停止播放猫叫
            clip.enabled = false;
            GameObject.Destroy(apple);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        //开门以后，且旁边没有水果，开始播放猫叫
        if (!isTrigger && other.gameObject == door)
        {
            clip.enabled = true;
            clip.Play();//循环播放
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == apple || other.gameObject == mother)
        {
            isTrigger = true; //苹果扔到猫旁边
            Interact(null);//
        }
    }

    public override void Interact()
    {

    }
}