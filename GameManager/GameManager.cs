using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 状态值
    /// </summary>
    Dictionary<string, int> values = new Dictionary<string, int>();
    /// <summary>
    /// 工具获取状态
    /// </summary>
    Dictionary<string, GameObject> Tools = new Dictionary<string, GameObject>();
    public UIManager UIManager;
    public TaskManager TaskManager;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GetPlayer();
        UIManager = UIManager.GetInstance();
        InitTab();
        TaskManager = TaskManager.GetInstance();
        InitTools();



    }
    void InitTools()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Tool"))
        {
            o.GetComponent<Tool>().Init();
        }
    }
    void InitTab()
    {       
        //添加数值
        values.Add("钱", 5);
        values.Add("心跳", 100);
        values.Add("紧张度", 500);
        //添加道具
        Tools.Add("手机", null);
        UIManager.UImain["Tab"].UI.GetComponent<TabUI>().Init(values, Tools);
    }

    /// <summary>
    /// 设置道具获取状态
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetTool(string key, GameObject value)
    {

        Tools[key] = value;
    }
    /// <summary>
    /// 道具获取状态
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool GetTool(string key)
    {
        return Tools[key] != null;
    }
    /// <summary>
    /// 获得参数值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int Getvalue(string key)
    {
        return values[key];
    }
    /// <summary>
    /// 设置参数值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Setvalue(string key, int value)
    {
        values[key] = value;
    }
    bool GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        return player != null;
    }


    /// <summary>
    /// Tab界面更新
    /// </summary>
    public void ShowInTab()
    {

        UIManager.UImain["Tab"].UI.GetComponent<TabUI>().Updatevalues(values, Tools); ;

    }

    // Update is called once per frame
    void Update()
    {




    }
}

