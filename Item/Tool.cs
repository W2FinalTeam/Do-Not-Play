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

    // Update is called once per frame
    void Update()
    {
        
    }
    public override  void Init()
    {
        myTransform = this.transform;
    }
    public override void Destory()
    {
        Destroy(gameObject);
    }

    public abstract GameObject PickUpItem(Transform parent);


    public abstract void Use();
    public abstract void UnUse();
}
