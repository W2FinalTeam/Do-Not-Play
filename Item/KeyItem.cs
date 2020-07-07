using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 会被检测是否在原来位置的Item
/// </summary>
public class KeyItem : BaseItem
{
    private bool BeTaken;
    public bool IsChecked = false;
    public override void Destory() { }
    public override void Init() 
    {
        BeTaken = false;
    }
    /// <summary>
    /// 角色取走
    /// </summary>
    public void PlayerTookMe()
    {
        BeTaken = true;
        Debug.Log("PlayerTookMe");
    }
    /// <summary>
    /// 母亲放回
    /// </summary>
    public void ItemReturn()
    {
    
    BeTaken = false;    }
    /// <summary>
    /// 查找物品是否丢失
    /// </summary>
    /// <returns></returns>
    public bool CheckItemTaken()
    {
        return BeTaken;
    }
  
}
