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
    public override void Init()
    {
        myTransform = this.transform;
    }
    public override void Destory()
    {
        Destroy(gameObject);
    }

    public abstract void Interact(GameObject inHandItem);
    public abstract void Interact();
}
