using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : MonoBehaviour
{
    private Boolean statement = true;
    public GameObject[] doors;
    
    // Start is called before the first frame update
    void Start()
    {
        
       // this.InterAct(this.gameObject);


    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            this.OpenDoor();
        }
    }
    public  void OpenDoor()
    {
       
            if (statement)
            {  foreach (GameObject door in doors)
            {
                door.GetComponent<Door>().open();
                statement = false;
            }
            }
            else
            {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Door>().close();
                statement = true;
            }
            }
        
    }
}
