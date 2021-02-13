using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class MenuManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager UIManager;
    public FirstPersonController FPC;
    private void Start()
    {
        UIManager = UIManager.GetInstance();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        FPC = gameManager.player.GetComponent<FirstPersonController>();
    }
    //UI事件
    public void Continue()
    {
        UIManager.SetUI("菜单", false);
        if (!UIManager.UImain["Tab"].isShow)
        {
            FPC.m_MouseLook.lockCursor = true;
            Cursor.visible = false;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Team()
    {
        UIManager.SetUI("制作团队", true);
    }
    public Slider slider;
    public void VolumeChange()
    {
        AudioListener.volume = slider.value;
    }
    public void MaxVolumChange()
    {
        if (slider.value > 0)
            slider.value = 0;
        else
            slider.value = 1;
    }
    public void Load()
    {
        UIManager.SetUI("菜单", false);
        StartCoroutine(GMLoad());
    }
    IEnumerator GMLoad()
    {
        FPC.enabled = false;
        gameManager.Load();
        yield return new WaitForSeconds(2f);
        FPC.enabled = true;
        FPC.m_MouseLook.lockCursor = true;
        Cursor.visible = false;
    }
}
