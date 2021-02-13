using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : BaseItem, ITool
{
    public bool isusing = false;
    public string task;

    public override void Init()
    {
        myTransform = this.transform;
        GameObject o = GameObject.Find(transform.name + "存放点");
        
        if (o != null)
        {
            transform.parent = o.transform.parent;
            transform.position = o.transform.position;
        }

    }
    public override void Destory()
    {
        Destroy(gameObject);
    }

    public abstract GameObject PickUpItem(Transform parent);

    public abstract void Use();
    public abstract void UnUse();
}
