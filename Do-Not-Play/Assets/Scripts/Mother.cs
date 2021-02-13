
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储关于母亲AI的一些变量
/// 增加听力变量
/// </summary>
public class Mother : MonoBehaviour
{
    #region 变量
    //发现物体被拿走
    public GameObject lostItem;
    //发现被拿走的物体
    public GameObject FindItem;

    public float StaticTension;
    public float TempTension;
    public float WholeTension;

    public Vector3 Destination = Vector3.zero;
    private GameManager gameManager;

    public GameObject CurrentWayPoints;
    public Transform[] Positions;
    public int NowPoint = 1;
    public bool HearSomething;
    public Transform HearTarget;

    public bool TimeOver = false;

    private AudioSource footstep;
    private Animator animator;
    //脚步间隔时间
    float t;
    #endregion
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StaticTension = 0;
        TempTension = 50;
        StaticTension=gameManager.Getvalue("固定紧张度");
        TempTension=gameManager.Getvalue("可变紧张度");
        GetCurrentWaypoints(gameManager.LevelManager.level);
        footstep = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        FindItem = null;
        lostItem = null;
        TimeOver = false;
    }
    public void ChangeStaticTensionValue(int RiseValue)
    {
        StaticTension += RiseValue;
        if (StaticTension <= 0)
            StaticTension = 0;
        gameManager.Setvalue("固定紧张度", (int)StaticTension);
        gameManager.ShowInTab();
    }
    public void ChangeTempTensionValue(float RiseValue)
    {
        TempTension += RiseValue;
        if (TempTension <= 0)
            TempTension = 0;
        gameManager.Setvalue("可变紧张度", (int)TempTension);
        gameManager.ShowInTab();
    }
    //获得当前关卡的巡逻点
    public void GetCurrentWaypoints(int level)
    {
        if (level >= 2)
            level = 1;
        CurrentWayPoints = GameObject.Find("AllLevelWayPoints/Level"+level+1);
        
        switch (level)
        {
            case 0:
                CurrentWayPoints = GameObject.Find("AllLevelWayPoints/Level1");
                break;
            case 1:
                CurrentWayPoints = GameObject.Find("AllLevelWayPoints/Level2");
                break;
            default:
                break;
        }
        Positions = CurrentWayPoints.GetComponentsInChildren<Transform>();
        NowPoint = 1;
    }
    public bool IsCanHear()
    {
        return HearSomething;
    }
    // Update is called once per frame
    void Update()
    {
        WholeTension = StaticTension + TempTension;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            t += Time.deltaTime;
            if (t >= 0.7)
            {
                footstep.PlayOneShot(footstep.clip);
                t = 0;
            }
        }
        else
        {
            t = 0.7f;
        }
    }

}
