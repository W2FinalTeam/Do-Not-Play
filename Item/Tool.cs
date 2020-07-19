using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : BaseItem, ITool
{
    public bool isusing = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override  void Init()
    {
        
        GameObject o = GameObject.Find(transform.name + "存放点");
     
        if (o != null)
            transform.position = o.transform.position;
    }
    public override void Destory()
    {
        Destroy(gameObject);
    }

    public abstract GameObject PickUpItem(Transform parent);


    public abstract void Use();
    public abstract void UnUse();
}
