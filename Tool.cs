using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : BaseItem, ITool
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
}
