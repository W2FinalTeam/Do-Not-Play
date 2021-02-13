using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class TabUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Itemtext;
    public GameObject Itembutton;
    public Dictionary<string, int> values = new Dictionary<string, int>();
    public Dictionary<string, GameObject> Tools = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> UIList = new Dictionary<string, GameObject>();
    public GameObject parent;

    public void Init(Dictionary<string, int> values, Dictionary<string, GameObject> Tools)
    {

        this.values = values;
        this.Tools = Tools;
        foreach (var item in values)
        {
            AddItemtext(item.Key, item.Value);
        }
        foreach (var item in Tools)
        {
            AddItembutton(item.Key, item.Value);
        }

    }
    /// <summary>
    /// 从GameManager调用，更新数值
    /// </summary>
    /// <param name="values"></param>
    /// <param name="Tools"></param>
    public void Updatevalues(Dictionary<string, int> values, Dictionary<string, GameObject> Tools)
    {
        this.values = values;
        this.Tools = Tools;
        UpdateUI();
    }
    /// <summary>
    /// UI界面更新
    /// </summary>
    void UpdateUI()
    {
        foreach (var item in values)
        {
            UIList[item.Key].GetComponent<Text>().text = $"{item.Key}:{item.Value}";
        }
        foreach (var item in Tools)
        {
            Transform t;
            if (item.Value == null||item.Value.name=="")
            {
                if (UIList.ContainsKey(item.Key))
                {
                    UIList.Remove(item.Key);
                }
                if((t= transform.Find("Viewport/Content/" + item.Key + "UI")) != null)
                {
                    DestroyImmediate(t.gameObject);
                }
                continue;
            }
            else
                AddItembutton(item.Key, item.Value);
        }
    }
    /// <summary>
    /// 增加一个文本组件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddItemtext(string key, int value)
    {
        GameObject temp = Instantiate(Itemtext) as GameObject;
        temp.name = key;
        temp.transform.GetComponent<Text>().text = $"{key}:{value}";
        temp.GetComponent<Transform>().SetParent(parent.GetComponent<Transform>(), false);
        if (!UIList.ContainsKey(key))
            UIList.Add(key, temp);
        else
        {
            UIList[key] = temp;
        }
    }
    /// <summary>
    /// 增加一个道具按钮
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddItembutton(string key, GameObject value)
    {
        if (!UIList.ContainsKey(key))
        {
            UIList.Add(key, value);
        }
        if ((transform.Find("Viewport/Content/" + key + "UI")) == null)
        {
            GameObject temp = Instantiate(Itembutton) as GameObject;
            temp.name = key + "UI";
            temp.GetComponent<Button>().onClick.AddListener(
                delegate ()
                {
                    this.UseTool(key);
                }
            );
            temp.transform.Find("Text").GetComponent<Text>().text = $"{key}";
            temp.GetComponent<Transform>().SetParent(parent.GetComponent<Transform>(), false);
        }
    }
    /// <summary>
    /// 按钮绑定使用道具
    /// </summary>
    /// <param name="key"></param>
    public void UseTool(string key)
    {
        if (Tools[key] != null)
        {
            Tools[key].GetComponent<Tool>().Use();
        }
    }
}
