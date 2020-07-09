using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class LevelManager : BaseManager<LevelManager>
{
    /// <summary>
    /// 当前关卡
    /// </summary>
    public int level = 0;
    /// <summary>
    /// 关卡任务预置
    /// </summary>
    List<List<String>> TaskFeb = new List <List<String>>();
    /// <summary>
    /// 关卡道具预置
    /// </summary>
    List< List<String>> ToolFeb = new List<List<String>>();

    /// <summary>
    /// 当前关卡任务完成状态
    /// </summary>
    Dictionary<string, bool> task = new Dictionary<string, bool>();
    /// <summary>
    /// 当前关卡道具
    /// </summary>
    List<String> Tool = new List<String>();
    // Start is called before the first frame update

    /// <summary>
    /// 加载本地数据
    /// </summary>
    void LoadData()
    {
        JsonData data;
        using (StreamReader r = new StreamReader("data.json"))

        {

            string JsonData = r.ReadToEnd();
            data = JsonMapper.ToObject(JsonData);
        }
        TaskFeb = JsonMapper.ToObject<List<List<String>>>(data["TaskFeb"].ToJson());
        ToolFeb = JsonMapper.ToObject<List<List<String>>>(data["ToolFeb"].ToJson());
    }
    /// <summary>
    /// 初始化关卡
    /// </summary>
    /// <param name="level"></param>
    public void Init(int level) {
        this.level = level;
        //加载数据
        LoadData();
        //初始化任务
        task.Clear();
        List<String> tasks = TaskFeb[level];
        foreach(String task in tasks)
        {
            this.task.Add(task, false);
            Debug.Log(task);
        }
        //初始化道具
        Tool.Clear();
        List<String> Tools = ToolFeb[level];
        foreach (String tool in Tools)
        {
            this.Tool.Add(tool);
            Debug.Log(tool);
        }
    }
    /// <summary>
    /// 获得当前任务
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<string, bool> GetTaskList()
    {
        return task;
    }
    /// <summary>
    /// 加入新关卡任务
    /// </summary>
    /// <param name="level"></param>
    void AddnewTask(int level)
    {
        List<String> tasks = TaskFeb[level];
        foreach (String task in tasks)
        {
            this.task.Add(task, false);
            Debug.Log(task);
        }
    }
    /// <summary>
    /// 加入新关卡道具
    /// </summary>
    /// <param name="level"></param>
    void AddnewTool(int level)
    {
        List<String> Tools = ToolFeb[level];
        foreach (String tool in Tools)
        {
            this.Tool.Add(tool);
            Debug.Log(tool);
        }
    }


    public bool HaveTask(string key)
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
        if (value)
        {
            if (TaskCheck())
            {
                Finish();
            }
        }
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
    bool TaskCheck()
    {

        foreach (var item in task)
        {
            if (!item.Value)
            {
                return false;
            }
        }
        return true;
    }
     void Finish()
    {
        Debug.Log("通过" + level);
        level += 1;
        Debug.Log("载入" + level);
        AddnewTask(level);
        AddnewTool(level);
        
    }
}
