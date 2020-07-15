using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Itemtext;
	public GameObject Itembutton;
	public Dictionary<string, int> values=new Dictionary<string, int>();
	public Dictionary<string, GameObject> Tools = new Dictionary<string, GameObject>();
	public Dictionary<string, GameObject> UIList=new Dictionary<string, GameObject>();
	public GameObject parent;
    void Start()
    {
		
	}
	public void Init (Dictionary<string, int> values, Dictionary<string, GameObject> Tools)
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
	public void Updatevalues(Dictionary<string, int> values,Dictionary<string,GameObject> Tools)
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
		foreach(var item in values)
		{
			string key = item.Key;
			int value = item.Value;
			UIList[key].GetComponent<Text>().text = $"{key}:{value}";
		}
		foreach (var item in Tools)
		{
			string key = item.Key;
			GameObject value = item.Value;
			UIList[key].GetComponent<Button>().enabled = value != null;
		}

	}
	/// <summary>
	/// 增加一个文本组件
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	 void AddItemtext(string key,int value)
	{
		GameObject temp = Instantiate(Itemtext) as GameObject;
		temp.transform.GetComponent<Text>().text = $"{key}:{value}";
		temp.GetComponent<Transform>().SetParent(parent.GetComponent<Transform>(), false);
	
		UIList.Add(key,temp);


	}
	/// <summary>
	/// 增加一个道具按钮
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	 void AddItembutton(string key, GameObject value)
	{
		GameObject temp = Instantiate(Itembutton) as GameObject;
		temp.GetComponent<Button>().onClick.AddListener(
			delegate () {
				UseTool(key);
			}
		);
		temp.transform.Find("Text").GetComponent<Text>().text = $"{key}";
		temp.GetComponent<Transform>().SetParent(parent.GetComponent<Transform>(), false);

		UIList.Add(key, temp);


	}
	/// <summary>
	/// 按钮绑定使用道具
	/// </summary>
	/// <param name="key"></param>
	void UseTool(string key)
	{
		if (Tools[key] != null)
		{
			Tools[key].GetComponent<Tool>().Use();
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }

}
