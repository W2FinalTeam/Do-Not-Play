using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangeableItem : BaseItem, IChangeableItem
{
    public override void Init()
    {
        transform.position = myTransform.position;
        transform.rotation = myTransform.rotation;
    }
    public override void Destory()
    {
        Destroy(gameObject);
    }

    public abstract void Interact(GameObject inHandItem);
    public abstract void Interact();
}
