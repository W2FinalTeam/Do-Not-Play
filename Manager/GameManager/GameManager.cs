using System;
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
     Dictionary<string, int> values=new Dictionary<string, int>();
    /// <summary>
    /// 工具获取状态
    /// </summary>
     Dictionary<string, GameObject> Tools = new Dictionary<string, GameObject>();
    public UIManager UIManager;
    public LevelManager LevelManager;
    public GameObject player;
    public GameObject mother;
    bool firsttime = true;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        GetPlayer();
        UIManager=UIManager.GetInstance();
        LevelManager = LevelManager.GetInstance();

        LoadLevel(0);





    }
    /// <summary>
    /// 重新开始本关
    /// </summary>
    public void Restart()
    {
        LoadLevel(LevelManager.level);
    }
    /// <summary>
    /// 加载关卡(道具重置)
    /// </summary>
    /// <param name="first">第一次执行</param>
    /// <param name="level"></param>
    
    public void LoadLevel(int level)
    {
        DestroyAllTool();
        InitTab(firsttime);
        
        LevelManager.Init(level);
    }
    /// <summary>
    /// 存档
    /// </summary>
    public  void Save()
    {
        SaveData save=new SaveData();
        save.SavetoFile("save1");
    }
    /// <summary>
    /// 加载存档
    /// </summary>
    public void Load()
    {
        SaveData save = new SaveData();
        save.LoadSaveFile("save1");
        LoadLevel(save.level);

        GetPlayer();
        player.transform.position = save.PlayerLocation.GetPosition();
        player.transform.rotation = save.PlayerLocation.GetRotation();
        mother.transform.position = save.NPCLocation.GetPosition();
        mother.transform.rotation = save.NPCLocation.GetRotation();
      
        values = save.values;
        LevelManager.task = save.task;
        foreach(var item in save.Tools)
        {
            
            GameObject o = GameObject.Find(item.Key + "(Clone)");
            if (o != null)
            {
        
                o.transform.position = save.ToolLocation[item.Key + "(Clone)"].GetPosition();
                o.transform.rotation = save.ToolLocation[item.Key + "(Clone)"].GetRotation();
               
            }
        
           
            
            if (item.Value)
            {
                Tools[item.Key] = o;
            }
        }
        

    }


    /// <summary>
    /// 重置Tab
    /// </summary>
    /// <param name="first">第一次执行</param>
     void InitTab(bool first)
    {
        if (first)
        {
            //添加数值
            values.Add("钱", 5);
            values.Add("心跳", 100);
            values.Add("紧张度", 0);
            //添加道具
            Tools.Add("手机", null);
            Tools.Add("遥控汽车", null);
            UIManager.UImain["Tab"].UI.GetComponent<TabUI>().Init(values, Tools);
            firsttime = false;
        }
        else
        {
            values["钱"]= 5;
            values["心跳"] =100;
            values["紧张度"] = 0;
         
            Tools["手机"]=null;
            UIManager.UImain["Tab"].UI.GetComponent<TabUI>().Updatevalues(values, Tools);
        }
    }
    /// <summary>
    /// 销毁所有道具
    /// </summary>
    /// <returns></returns>
    void DestroyAllTool()
    {
        GameObject[] tools = GameObject.FindGameObjectsWithTag("Tool");
        foreach (var item in tools)
        {
            item.GetComponent<Tool>().Destory();
        }
    }
    public Dictionary<string, int> GetValuesList()
    {
        return values;
    }
    public Dictionary<string,GameObject> GetToolList()
    {
        return Tools;
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
        mother = GameObject.FindGameObjectWithTag("Mother");
        return player != null;
    }


    /// <summary>
    /// Tab界面更新
    /// </summary>
    public  void ShowInTab()
    {

      UIManager.UImain["Tab"].UI.GetComponent<TabUI>().Updatevalues(values, Tools); ;
    
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Save();
            
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
           Load();

        }

    }
}

