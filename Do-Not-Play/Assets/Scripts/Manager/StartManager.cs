using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public string sceneName;
    public GameObject canvas;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
    }
    public void StartGame()
    {
        GlobalScene.nextScene = sceneName;
        GlobalScene.reStart = false;
        SceneManager.LoadScene("LoadingScene");
    }
    public void RestartGame()
    {
        GlobalScene.nextScene = sceneName;
        GlobalScene.reStart = true;
        SceneManager.LoadScene("LoadingScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Team()
    {
        canvas.transform.Find("制作团队").gameObject.SetActive(true);
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
}
