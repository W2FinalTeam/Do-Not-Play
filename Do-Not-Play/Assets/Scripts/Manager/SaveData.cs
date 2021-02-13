using BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
[System.Serializable]
public class SaveData
{

    GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    GameObject NPC = GameObject.FindGameObjectWithTag("Mother");

    public class Location
    {

        public String position;
        public String rotation;
        public Location(Vector3 a, Quaternion b)
        {
            position = a.x.ToString("f2") + " " + a.y.ToString("f2") + " " + a.z.ToString("f2");
            rotation = b.x.ToString("f2") + " " + b.y.ToString("f2") + " " + b.z.ToString("f2") + " " + b.w.ToString("f2");
        }
        public Location(string a, string b)
        {
            position = a;
            rotation = b;
        }
        public Vector3 GetPosition()
        {
            string[] str = position.Split(' ');

            if (str.Length != 3)
            {
                Debug.LogError("The 'posString' is fails...");
                return Vector3.zero;
            }
            return new Vector3(float.Parse(str[0]), float.Parse(str[1]), float.Parse(str[2]));
        }
        public Quaternion GetRotation()
        {
            string[] str = rotation.Split(' ');
            if (str.Length != 4)
            {
                Debug.LogError("The 'posString' is fails...");
                return new Quaternion(0, 0, 0, 0);
            }
            return new Quaternion(float.Parse(str[0]), float.Parse(str[1]), float.Parse(str[2]), float.Parse(str[3]));
        }
    }
    /// <summary>
    /// 关卡
    /// </summary>
    public int level;
    /// <summary>
    /// 状态值
    /// </summary>
    public Dictionary<string, int> values = new Dictionary<string, int>();
    /// <summary>
    /// 工具获取状态
    /// </summary>
    public Dictionary<string, bool> Tools = new Dictionary<string, bool>();
    /// <summary>
    /// 当前关卡任务完成状态
    /// </summary>
    public Dictionary<string, bool> task = new Dictionary<string, bool>();
    /// <summary>
    /// 道具位置
    /// </summary>
    public Dictionary<string, Location> ToolLocation = new Dictionary<string, Location>();
    /// <summary>
    /// 玩家坐标
    /// </summary>
    public Location PlayerLocation;
    /// <summary>
    /// 母亲坐标
    /// </summary>
    public Location NPCLocation;
    /// <summary>
    /// 紧张度
    /// </summary>
    public double StaticTension;
    public double TempTension;
    public double WholeTension;
    public void SavetoFile(string filename)
    {

        Dictionary<string, GameObject> toolslist = gameManager.GetToolList();

        foreach (var item in toolslist)
        {

            if (item.Value != null)
            {

                Tools.Add(item.Key, true);

            }
            else
            {
                Tools.Add(item.Key, false);
            }
        }
        GameObject[] tools = GameObject.FindGameObjectsWithTag("Tool");
        foreach (var item in tools)
        {
            if (ToolLocation.ContainsKey(item.name))
                ToolLocation[item.name] = new Location(item.transform.position, item.transform.rotation);
            else
                ToolLocation.Add(item.name, new Location(item.transform.position, item.transform.rotation));
        }
        values = gameManager.GetValuesList();
        task = gameManager.LevelManager.GetTaskList();
        PlayerLocation = new Location(player.transform.position, player.transform.rotation);
        NPCLocation = new Location(NPC.transform.position, player.transform.rotation);
        level = gameManager.LevelManager.level;
        StaticTension = NPC.GetComponent<Mother>().StaticTension;
        TempTension = NPC.GetComponent<Mother>().TempTension;
        WholeTension = NPC.GetComponent<Mother>().WholeTension;
        Dictionary<string, object> temp = new Dictionary<string, object>();
        temp.Add("values", values);
        temp.Add("level", level);
        temp.Add("Tools", Tools);
        temp.Add("ToolLocation", ToolLocation);
        temp.Add("task", task);
        temp.Add("PlayerLocation", PlayerLocation);
        temp.Add("NPCLocation", NPCLocation);
        temp.Add("TempTension", TempTension);
        temp.Add("StaticTension", StaticTension);
        temp.Add("WholeTension", WholeTension);

        string jsonStr = JsonMapper.ToJson(temp);
        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        var ss = reg.Replace(jsonStr, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        // 新建一个StreamWriter，并将字符串写入
        StreamWriter sw = new StreamWriter(filename, false, Encoding.GetEncoding("utf-8"));
        sw.Write(ss);
        // 关闭StreamWriter
        sw.Close();

  //      Debug.Log("新建存档成功");
        return;
    }
    public void LoadSaveFile(string filename)
    {
        if (!File.Exists(filename))
            return;

        //创建一个StreamReader，用来读取流
        StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("utf-8"));
        //将读取到的流赋值给jsonStr
        string jsonStr = sr.ReadToEnd();
        //关闭
        sr.Close();

        JsonData temp = JsonMapper.ToObject(jsonStr);

        this.values = JsonMapper.ToObject<Dictionary<string, int>>(temp["values"].ToJson());
        this.task = JsonMapper.ToObject<Dictionary<string, bool>>(temp["task"].ToJson());
        this.ToolLocation = new Dictionary<string, Location>();
        JsonData ToolLocationjson = temp["ToolLocation"];
        List<string> keys = ToolLocationjson.Keys.ToList<string>();
        for (int i = 0; i < keys.Count; i++)
        {
            ToolLocation.Add(keys[i], new Location(ToolLocationjson[keys[i]]["position"].ToString(), ToolLocationjson[keys[i]]["rotation"].ToString()));
        }
        this.Tools = JsonMapper.ToObject<Dictionary<string, bool>>(temp["Tools"].ToJson());
        this.NPCLocation = new Location(temp["NPCLocation"]["position"].ToString(), temp["NPCLocation"]["rotation"].ToString());
        this.PlayerLocation = new Location(temp["PlayerLocation"]["position"].ToString(), temp["PlayerLocation"]["rotation"].ToString());
        this.level = (int)temp["level"];
        this.StaticTension = (double)temp["StaticTension"];
        this.TempTension = (double)temp["TempTension"];
        this.WholeTension = (double)temp["WholeTension"];
    }
}