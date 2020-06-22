using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmItem : BaseItem 
{
    private AudioSource clip;
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
    }
    protected override void Destory()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MoveableItem")|| other.CompareTag("Player")|| other.CompareTag("Tool"))
            PlayWaringSound();
    }
    private void PlayWaringSound()
    {
        clip.PlayOneShot((AudioClip)Resources.Load("警报声"));
    }
}
