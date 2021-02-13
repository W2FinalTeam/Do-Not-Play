using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Linq;
using UnityEngine.Video;
using UnityEngine.UI;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;

public class LevelManager : BaseManager<LevelManager>
{
    string path = Application.streamingAssetsPath + "/data.json";
    /// <summary>
    /// 当前关卡
    /// </summary>
    public int level = 0;
    /// <summary>
    /// 关卡任务预置
    /// </summary>
    List<List<String>> TaskFeb = new List<List<String>>();
    /// <summary>
    /// 关卡道具预置
    /// </summary>
    List<List<String>> ToolFeb = new List<List<String>>();

    /// <summary>
    /// 当前关卡任务完成状态
    /// </summary>
    public Dictionary<string, bool> task = new Dictionary<string, bool>();
    /// <summary>
    /// 当前关卡道具
    /// </summary>
    public List<String> Tool = new List<String>();
    /// <summary>
    /// 当前任务描述
    /// </summary>
    Text currentTaskText;
    /// <summary>
    /// 任务检测
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public bool CheckTask(String task)
    {
        if (task == null || task == "")
            return true;
        if (HaveTask(task) && !GetTask(task))
        {
            SetTask(task, true);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 加载本地数据
    /// </summary>
    void LoadData()
    {
        JsonData data;
        using (StreamReader r = new StreamReader(path))
        {
            string JsonData = r.ReadToEnd();
            data = JsonMapper.ToObject(JsonData);
        }
        TaskFeb = JsonMapper.ToObject<List<List<String>>>(data["TaskFeb"].ToJson());
        ToolFeb = JsonMapper.ToObject<List<List<String>>>(data["ToolFeb"].ToJson());
    }
    /// <summary>
    /// 读档加载关卡
    /// </summary>
    /// <param name="level"></param>
    public void Load(int level)
    {
        currentTaskText = UIManager.GetInstance().UImain["任务进度"].UI.transform.Find("Text").GetComponent<Text>();
        this.level = level;
    }
    /// <summary>
    /// 初始化关卡
    /// </summary>
    /// <param name="level"></param>
    public void Init(int level, int state = 0)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentTaskText = UIManager.GetInstance().UImain["任务进度"].UI.transform.Find("Text").GetComponent<Text>();
        this.level = level;
        currentTaskText.text = "";
        //加载数据
        LoadData();
        //初始化任务
        task.Clear();
        if (TaskFeb.Count > level)
        {
            List<String> tasks = TaskFeb[level];
            foreach (String task in tasks)
            {
                currentTaskText.text += task;
                this.task.Add(task, false);
            }
        }
        //初始化道具
        Tool.Clear();
        if (ToolFeb.Count > level)
        {
            for (int i = 0; i <= level; i++)
            {
                List<String> Tools = ToolFeb[i];
                foreach (String tool in Tools)
                {
                    GameObject temp;
                    this.Tool.Add(tool);
                    //创建道具
                    if (GameObject.Find(tool))
                    {
                        GameManager.DestroyImmediate(GameObject.Find(tool));
                        //Debug.Log("LevelDestroy" + tool);
                    }
                    //Debug.Log(tool);
                    temp = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + tool));
                    temp.name = tool;
                    temp.GetComponent<Tool>().Init();

                    if (i < level)
                    {
                        if (gameManager.Tools.ContainsKey(tool))
                            gameManager.Tools[tool] = temp;
                        else
                            gameManager.Tools.Add(tool, temp);
                        temp.transform.position = new Vector3(100, 0, 0);

                        gameManager.p.ItemList.Add(temp.name);
                        if (temp.GetComponent<Timer>())
                            temp.GetComponent<Timer>().enabled = false;
                    }
                    //调用道具Init
                    //temp.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < level; i++)
            {
                List<String> Tools = ToolFeb[i];
                foreach (String tool in Tools)
                {
                    GameObject temp;
                    this.Tool.Add(tool);
                    //创建道具
                    if (GameObject.Find(tool))
                    {
                        GameManager.DestroyImmediate(GameObject.Find(tool));
                        //Debug.Log("LevelDestroy" + tool);
                    }
                    //Debug.Log(tool);
                    temp = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + tool));
                    temp.name = tool;
                    temp.GetComponent<Tool>().Init();
                    if (gameManager.Tools.ContainsKey(tool))
                        gameManager.Tools[tool] = temp;
                    else
                        gameManager.Tools.Add(tool, temp);
                    temp.transform.position = new Vector3(100, 0, 0);
                    if (!gameManager.p.ItemList.Contains(tool))
                        gameManager.p.ItemList.Add(temp.name);
                    if (temp.GetComponent<Timer>())
                        temp.GetComponent<Timer>().enabled = false;
                }
            }
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
        currentTaskText.text = "";
        List<String> tasks = TaskFeb[level];
        foreach (String task in tasks)
        {
            currentTaskText.text += task;
            this.task.Add(task, false);
        }
    }
    /// <summary>
    /// 加入新关卡道具
    /// </summary>
    /// <param name="level"></param>
    void AddnewTool(int level)
    {
        if (ToolFeb.Count > level)
        {
            List<String> Tools = ToolFeb[level];
            foreach (String tool in Tools)
            {
                this.Tool.Add(tool);
                //创建道具
                GameObject temp = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + tool));
                temp.name = tool;
                //调用道具Init
                temp.GetComponent<Tool>().Init();
            }
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
    public void Finish()
    {

        //Debug.Log("通过" + level);
        level += 1;
        if (level == TaskFeb.Count)
        {
            return;
        }
        else
        {
            AddnewTask(level);
            AddnewTool(level);
            //Debug.Log("载入" + level);
        }

    }
}
