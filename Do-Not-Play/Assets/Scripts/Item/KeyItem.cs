using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 会被检测是否在原来位置的Item
/// </summary>
//被修改，需同步
public class KeyItem : BaseItem
{
    private bool BeTaken;
    //关键位置没有指定物品的变量
    public bool IsChecked = false;
    //检验遇到的关键物体是不是在原位的变量
    public bool IsChecked2 = true;
    public override void Destory() { }
    public override void Init() 
    {
        BeTaken = false;
        IsChecked = false;
        IsChecked2 = true;
    }

    /// <summary>
    /// 角色取走
    /// </summary>
    public void PlayerTookMe()
    {
        BeTaken = true;
    }


    /// <summary>
    /// 角色丢掉
    /// </summary>
    public void PlayerThrowMe()
    {
        IsChecked2 = false;
   //     Debug.Log("PlayerThrowMe");
    }


    /// <summary>
    /// 母亲放回
    /// </summary>
    public void ItemReturn()
    {
        BeTaken = false;
        IsChecked2 = true;
        IsChecked = false;
    }

    /// <summary>
    /// 查找物品是否丢失
    /// </summary>
    /// <returns></returns>
    public bool CheckItemTaken()
    {
        return BeTaken;
    }
    /// <summary>
    /// 查找物品是否被丢弃
    /// </summary>
    /// <returns></returns>
    public bool CheckItemThrown()
    {
        return !IsChecked2;
    }
}
