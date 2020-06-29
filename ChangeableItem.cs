using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ChangeableItem : BaseItem, IChangeableItem
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
