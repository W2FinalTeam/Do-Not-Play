using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 任务完成状态
    /// </summary>
    Dictionary<string, bool> task = new Dictionary<string, bool>();
    /// <summary>
    /// 状态值
    /// </summary>
     Dictionary<string, int> values=new Dictionary<string, int>();
    /// <summary>
    /// 工具获取状态
    /// </summary>
     Dictionary<string, GameObject> Tools = new Dictionary<string, GameObject>();
    public GameObject TabUI;
    /// <summary>
    /// ui显示状态
    /// </summary>
    bool TabShow = false;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GetPlayer();

        //添加数值
        values.Add("钱", 5);
        values.Add("心跳", 100);
        values.Add("紧张度", 500);
        //添加道具
        Tools.Add("手机", null);
        //添加任务
        task.Add("获得手机", false);

        TabUI.GetComponent<TabUI>().Init(values,Tools);
    }
    public  bool HaveTask(string key)
    {
        return task.ContainsKey(key);
    }
    
    /// <summary>
    /// 设置任务完成状态
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetTask(string key, bool value)
    {

        task[key] = value;
    }
    /// <summary>
    /// 获取任务完成状态
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool GetTask(string key)
    {
        return task[key];
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
        return Tools[key]!=null;
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
    public void Setvalue(string key,int value)
    {
        values[key] = value;
    }
      bool GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        return player != null;
    }
    /// <summary>
    /// 设置UI界面显示状态
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
     bool SetTab(bool State)
    {
        if (State)
        {
            if (TabShow)
                return false;
            
            if (GetPlayer())
            {
                TabShow = true;
                TabUI.SetActive(true);
                player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Cursor.visible = false;
                return true;
            }
            return false;
            
           
        }
        else
        {
            if (!TabShow)
                return false;
           
            if (GetPlayer())
            {
                TabShow = false;
                TabUI.SetActive(false);
                player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
                Cursor.visible = true;
                return true;
            }
            return false;
            
            
        }
    }
    void KeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetTab(!TabShow);
        }
    }
    /// <summary>
    /// UI界面更新
    /// </summary>
    void ShowInTab()
    {
        if (TabShow)
        {
            TabUI.GetComponent<TabUI>().Updatevalues(values,Tools);
        }
    }
    bool TaskCheck()
    {
        
        foreach(var item in task)
        {
            if (!item.Value)
            {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (TaskCheck())
        {
            Debug.Log("过关");
           
        }
        KeyEvent();
        ShowInTab();

    }
}

