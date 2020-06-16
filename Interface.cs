using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlayer
{
    /// <summary>
    /// 射线检测 Item layer层。
    /// </summary>
    void AxisAnalysis();//gameObject.interact(player)
}
/// <summary>
/// 可拾取道具 数据成员 bool InHand(是否在人物手中)
/// </summary>
interface IMoveableItem
{
    /// <summary>
    /// 播放物品掉落音频
    /// </summary>
    void PlayDropSound();

    /// <summary>
    /// 按下G键向人物前方投掷手中物品
    /// </summary>
    /// <param name="inHand">手中是否有物品</param>
    void ThrowItem(bool inHand);

    /// <summary>
    /// 将道具显示在手上
    /// </summary>
    /// <param name="inHand">手中是否有物品</param>
    void ShowItemInHand(bool inHand);

    /// <summary>
    /// 按下E键拾取物品
    /// </summary>
    void PickUpItem(GameObject role);

}
/// <summary>
/// 永久性获得道具 可以通过菜单使用
/// </summary>
interface ITool
{
    /// <summary>
    /// 按下E键拾取物品
    /// </summary>
    void PickUpItem();

    /// <summary>
    /// 使用这个物品
    /// </summary>
    void Use();
    /// <summary>
    /// 取消使用这个物品
    /// </summary>
    void UnUse();

}
/// <summary>
/// 如可开关门，可开关柜子之类的物品
/// </summary>
interface IChangeableItem
{
    /// <summary>
    /// 对按键E进行相应的操作
    /// </summary>
    /// <param name="inHandItem">若玩家手中有对应的道具（钥匙）就可以打开锁上的房门</param>
    void Interact(GameObject inHandItem);
}
/*interface IPlayerHeartBeatSound
{
    /// <summary>
    /// 播放心跳音频，速度和大小与参数有关
    /// </summary>
    /// <param name="distance">和母亲的距离</param>
    /// <param name="tensionRate">紧张度 0-100</param>
    void PlayHeartBeatSound(int distance,int tensionRate);
}*/
interface IAlarmItem
{
    /// <summary>
    /// 碰撞事件（增加紧张度等）
    /// </summary>
    /// <param name="other">可以是道具也可以是玩家</param>
    void OnTriggerEnter(Collider other);
    /// <summary>
    /// 发出警报声
    /// </summary>
    void PlayWaringSound();
}
/*
interface ITask
{

}
interface ITaskManager
{

}
interface IScenceManager
{

}
/// <summary>
/// 管理各个房间，
/// </summary>
interface ILevelManager
{
    
}
interface ISaveManager
{

}*/