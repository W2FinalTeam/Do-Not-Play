using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UIItem
{
    public bool isShow;
    public GameObject UI;

    public UIItem(bool isShow, GameObject UI)
    {
        this.isShow = isShow;
        this.UI = UI;
    }
}
public class UIManager : BaseManager<UIManager>
{
    [SerializeField]
    public List<GameObject> UI = new List<GameObject>();
    public Dictionary<string, UIItem> UImain = new Dictionary<string, UIItem>();


    public UIManager()
    {
        GameObject canvas = GameObject.Find("Canvas");
        int num = canvas.transform.childCount;

        for (int i = 0; i < num; i++)
        {
            GameObject item = canvas.transform.GetChild(i).gameObject;

            UImain.Add(item.name, new UIItem(false, item));

        }
    }
    Player player;
    bool GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        return player != null;
    }
    /// <summary>
    /// 设置UI界面显示状态
    /// </summary>
    /// <param name="name"></param>
    /// <param name="State"></param>
    /// <returns></returns>
    public bool SetUI(string name, bool State)
    {
        if (!UImain.ContainsKey(name))
        {
            return false;
        }
        if (State)
        {
            if (UImain[name].isShow)
                return false;

            if (GetPlayer())
            {
                UImain[name].isShow = true;
                UImain[name].UI.SetActive(true);
                if (name == "Tab")
                {
                    player.FPC.m_MouseLook.lockCursor = false;
                    Cursor.visible = true;
                }
                return true;
            }
            return false;
        }
        else
        {
            if (!UImain[name].isShow)
                return false;

            if (GetPlayer())
            {
                UImain[name].isShow = false;
                UImain[name].UI.SetActive(false);
                if (name == "Tab")
                {
                    player.FPC.m_MouseLook.lockCursor = true;
                    Cursor.visible = false;
                }
                return true;
            }
            return false;
        }
    }
}
