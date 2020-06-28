using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    GameManager GameManager;
    /// <summary>
    /// 任务预置
    /// </summary>
    Dictionary<int,List<String>> TaskFeb = new Dictionary<int,List<String>>();
    /// <summary>
    /// 任务完成状态
    /// </summary>
    Dictionary<string, bool> task = new Dictionary<string, bool>();
    // Start is called before the first frame update
    void Start()
    {
      
      GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
          
      
    }
    /// <summary>
    /// 初始化任务
    /// </summary>
    /// <param name="level"></param>
    public void Init(int level) {
        task.Clear();
        List<String> tasks = TaskFeb[level];
        foreach(String task in tasks)
        {
            this.task.Add(task, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void Finish()
    {
        Debug.Log("通关");
    }
}
