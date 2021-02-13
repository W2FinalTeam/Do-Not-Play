using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 状态值
    /// </summary>
    public Dictionary<string, int> values = new Dictionary<string, int>();
    /// <summary>
    /// 工具获取状态
    /// </summary>
    public Dictionary<string, GameObject> Tools = new Dictionary<string, GameObject>();
    public UIManager UIManager;
    public LevelManager LevelManager;
    public GameObject player;
    public GameObject mother;
    bool firsttime = true;
    public Loading loading;


    public Player p;
    public Mother m;
    private void Awake()
    {
        Application.targetFrameRate = 80;
        GetPlayer();
        p = player.GetComponent<Player>();
        m = mother.GetComponent<Mother>();
        UIManager = UIManager.GetInstance();
        LevelManager = LevelManager.GetInstance();
        if (GlobalScene.reStart)
        {
            LoadLevel(0);
            Load();
        }
        else
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
    public void Save()
    {
        SaveData save = new SaveData();
        save.SavetoFile("save1");
    }
    /// <summary>
    /// 加载存档
    /// </summary>
    public void Load()
    {
        Time.timeScale = 1;
        UIManager.SetUI("菜单", false);
        UIManager.SetUI("Tab", false);
        SaveData save = new SaveData();
        save.LoadSaveFile("save1");

        DestroyAllTool();
        InitTab(firsttime);
        GetPlayer();
        player.transform.rotation = save.PlayerLocation.GetRotation();
        player.transform.position = save.PlayerLocation.GetPosition();

        mother.transform.position = save.NPCLocation.GetPosition();
        mother.transform.rotation = save.NPCLocation.GetRotation();
        m.GetCurrentWaypoints(save.level);
        values = save.values;

        LevelManager.task = save.task;
        LevelManager.Init(save.level, 1);
        this.Tools.Clear();
        GameObject[] temp = new GameObject[save.Tools.Count];
        int i = 0;
        foreach (var item in save.Tools)
        {
            //创建道具
            if (GameObject.Find(item.Key))
            {
                //Debug.Log("已有" + item.Key);
                DestroyImmediate(GameObject.Find(item.Key));
                //Debug.Log("gameDestroy" + GameObject.Find(item.Key));
            }
            temp[i] = Instantiate(Resources.Load<GameObject>("Prefabs/" + item.Key));
            temp[i].name = item.Key;
            temp[i].GetComponent<Tool>().Init();
            Debug.Log(temp[i].name);
            if (item.Value)
            {
                this.Tools.Add(item.Key, temp[i]);
                temp[i].transform.position = new Vector3(100, 0, 0);
                p.ItemList.Add(temp[i].name);
                if (temp[i].GetComponent<Timer>())
                    temp[i].GetComponent<Timer>().enabled = false;
                LevelManager.CheckTask(temp[i].GetComponent<Tool>().task);
            }
            else
            {
                this.Tools.Add(item.Key, null);
                temp[i].transform.position = save.ToolLocation[item.Key].GetPosition();
                temp[i].transform.rotation = save.ToolLocation[item.Key].GetRotation();
            }
            i++;
        }
        ShowInTab();
    }

    /// <summary>
    /// 重置Tab
    /// </summary>
    /// <param name="first">第一次执行</param>
    void InitTab(bool first)
    {
        if (first)
        {
            values.Add("固定紧张度", 0);
            values.Add("可变紧张度", 50);
            Tools.Add("手机", null);
            Tools.Add("手电筒", null);
            UIManager.UImain["Tab"].UI.GetComponent<TabUI>().Init(values, Tools);
            firsttime = false;
        }
        else
        {
            values["固定紧张度"] = 0;
            values["可变紧张度"] = 50;
            foreach (var t in LevelManager.Tool)
            {
                if (Tools.ContainsKey(t))
                {
                    Tools[t] = null;
                }
                else
                {
                    Tools.Add(t, null);
                }
                
            }
            ShowInTab();
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

            if (Tools.ContainsKey(item.name))
            {
                Tools.Remove(item.name);
                p.ItemList.Remove(item.name);
            }
            //item.GetComponent<Tool>().Destory();
            DestroyImmediate(item);
            //Debug.LogWarning("成功删除" + t);
        }
    }
    public Dictionary<string, int> GetValuesList()
    {
        return values;
    }
    public Dictionary<string, GameObject> GetToolList()
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
        mother = GameObject.FindGameObjectWithTag("Mother");
        return player != null;
    }
    /// <summary>
    /// Tab界面更新
    /// </summary>
    public void ShowInTab()
    {
        /*foreach(var item in Tools)
        {
            if (item.Value != null)
                Debug.Log(item.Key + "ccc" + item.Value.name);
            else
                Debug.Log(item.Key + "bbb");
        }*/
        UIManager.UImain["Tab"].UI.GetComponent<TabUI>().Updatevalues(values, Tools);
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
        if (Input.GetKeyDown(KeyCode.Tab) && !p.isUsing && !UIManager.UImain["菜单"].isShow)
        {
            ShowInTab();
            UIManager.SetUI("Tab", !UIManager.UImain["Tab"].isShow);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.SetUI("菜单", !UIManager.UImain["菜单"].isShow);
            if (UIManager.UImain["菜单"].isShow || UIManager.UImain["Tab"].isShow)
            {
                p.FPC.m_MouseLook.lockCursor = false;
                Cursor.visible = true;
            }
            else
            {
                p.FPC.m_MouseLook.lockCursor = true;
                Cursor.visible = false;
            }
        }
    }
}

