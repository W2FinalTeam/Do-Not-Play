using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储关于母亲AI的一些变量
/// 已修改，需同步
/// </summary>
public class Mother : MonoBehaviour
{
 #region 变量
    //发现物体被拿走
    public GameObject lostItem = null;
    //发现被拿走的物体
    public GameObject FindItem = null;
    //紧张度
    public float StaticTension;
    public float TempTension;
    public float WholeTension;
    //目标点
    public Vector3 Destination = Vector3.zero;
    private GameManager gameManager;
    //巡逻点
    public GameObject CurrentWayPoints;
    public Transform[] Positions;
    #endregion
    void Start()
    {
        TempTension = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StaticTension = gameManager.Getvalue("紧张度");
        GetCurrentWaypoints(gameManager.LevelManager.level);
        WholeTension = StaticTension + TempTension;
    }
    public void ChangeStaticTensionValue(int RiseValue)
    {
        StaticTension += RiseValue;
        if (StaticTension <= 0)
            StaticTension = 0;
        gameManager.Setvalue("紧张度", (int)StaticTension);
        WholeTension = StaticTension + TempTension;
    }
    public void ChangeTempTensionValue(float RiseValue)
    {
        TempTension += RiseValue;
        if (TempTension <= 0)
            TempTension = 0;
        WholeTension = StaticTension + TempTension;
    }
    //获得当前关卡的巡逻点
    public void GetCurrentWaypoints(int level)
    {
        switch (level)
        {
            case 0: CurrentWayPoints = GameObject.Find("AllLevelWayPoints/Level1");
                        break;
            case 1: CurrentWayPoints = GameObject.Find("AllLevelWayPoints/Level2");
                        break;
            default:
                        break;
        }
        Positions = CurrentWayPoints.GetComponentsInChildren<Transform>();
    }
}
