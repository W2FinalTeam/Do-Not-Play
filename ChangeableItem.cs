using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangeableItem : BaseItem, IChangeableItem
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
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
    public abstract void Interact();
}

