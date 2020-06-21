using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Tool : BaseItem//, ITool
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

    public void PickUpItem()
    {
        transform.localScale = Vector3.zero;
    }

    public abstract void Use();


    public abstract void UnUse();

}
