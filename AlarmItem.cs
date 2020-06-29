using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AlarmItem : BaseItem,IAlarmItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Init()
    {
        myTransform = this.transform;
    }
    public override void Destory()
    {
        Destroy(gameObject);
    }

    public abstract void OnTriggerEnter(Collider other);
    public abstract void PlayWaringSound();
}
