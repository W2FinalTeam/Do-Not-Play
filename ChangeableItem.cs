using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeableItem : BaseItem, IChangeableItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Init()
    {

    }
    /// <summary>
    /// 摧毁道具
    /// </summary>
    protected override void Destory()
    {

    }
   public void InterAct(GameObject target) //
    {
       if( target.tag == "Key")
        {
            target.GetComponent<Key>().OpenDoor();
        }
    }
   
}
