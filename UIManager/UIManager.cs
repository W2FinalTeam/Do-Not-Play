using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UIItem
{
    public bool isShow;
    public GameObject UI;
   
    public UIItem(bool isShow,GameObject UI)
    {
        this.isShow = isShow;
        this.UI = UI;
    }
}
public class UIManager : MonoBehaviour
{   [SerializeField]
    public List<GameObject> UI=new List<GameObject>();
    public Dictionary<string,UIItem> UImain=new Dictionary<string, UIItem>();
 
    GameManager GameManager;
    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach(GameObject item in UI)
        {
            UImain.Add(item.transform.name, new UIItem(false, item));
        }
       if(GameManager.UIManager!=null)
        GameManager.InitTab();
        else
        {
            GameManager.GetManager();
            GameManager.InitTab();
        }
 
    }
    /// <summary>
    /// 设置UI界面显示状态
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    GameObject player;
    bool GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        return player != null;
    }
    public bool SetUI(string name,bool State)
    {
       if(name=="Tab" && State == true)
        {
           
            GameManager.ShowInTab();
        }
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
                if (name == "Tab") { 
                player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
                Cursor.visible = false;
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
                    player.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
                Cursor.visible = true;
            }
            return true;
            }
            return false;


        }
    }

}
