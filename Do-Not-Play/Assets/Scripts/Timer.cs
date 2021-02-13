using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    TimeManager timer;
    public float needTime;
    public Text leftTime;

    private GameManager gameManager;
    float totalLeftTime;
    int min, sec;
    private void Start()
    {
        leftTime = UIManager.GetInstance().UImain["剩余时间"].UI.GetComponent<Text>();
        
    }
    private void OnEnable()
    {
        if (!LevelManager.GetInstance().HaveTask(transform.GetComponent<Tool>().task))
            this.enabled = false;
        timer = TimeManager.createTimer("LeftTimer");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.mother.GetComponent<Mother>().TimeOver = false;
        timer.startTiming(needTime, OnComplete, OnProcess);
    }
    private void OnComplete()
    {
        leftTime.text = "00:00";
        gameManager.mother.GetComponent<Mother>().TimeOver = true;
    }
    private void OnProcess(float p)
    {
        totalLeftTime = timer.GetLeftTime();
        min = (int)(totalLeftTime / 60);
        sec = (int)(totalLeftTime - min * 60);

        leftTime.text = (min > 10 ? min.ToString() : ("0" + min)) + ":" + (sec > 10 ? sec.ToString() : ("0" + sec)) ;
    }
    private void OnDisable()
    {
        if (timer != null)
            timer.destory();
        leftTime.text = "";
        gameManager.m.GetComponent<Mother>().TimeOver = false;
    }
}
