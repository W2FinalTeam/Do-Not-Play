using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    public FlashLight flashLight;
    public int flag = 0;
    public bool CanUse = true;
    private TimeManager timer;
    public int TimeForUse = 5;
    // Start is called before the first frame update
    void Start()
    {
        timer = TimeManager.createTimer("Timer");
        flashLight = gameObject.GetComponentInParent<FlashLight>();
        timer.startTiming(TimeForUse,TimeRunOut);
    }
    void Init()
    {
        flag = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (CanUse)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flag++;
                if (flag % 2 == 0)
                    {
                        flashLight.Use();
                        timer.connitueTimer();
                    }
                    else
                    {
                        flashLight.UnUse();
                        timer.pauseTimer();
                    }
                
            }
        }
    }
    void TimeStart()
    {
        
    }
    void OnProcess(float p)
    {
        //Debug.Log("on process " + p);
    }
    void TimeRunOut()
    {
        //Debug.Log("runOut");
        flashLight.UnUse();
        CanUse = false;
    }
}
