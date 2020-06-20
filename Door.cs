using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    private Transform m;
    // Start is called before the first frame update
    private Rigidbody rg;
    private Boolean statement = true;
    void Start()
    {
        m = gameObject.GetComponent<Transform>();
        rg = gameObject.GetComponent<Rigidbody>();
       
      //  this.InterAct();
    }

    // Update is called once per frame
    void Update()
    {  
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
         //   this.InterAct();
       // }
    }
    public void open()
    {
        rg.AddForce(m.forward * 1000f, ForceMode.Acceleration);
        rg.useGravity = true;
        statement = false;
        Debug.Log("Door");
    }
    public void close()
    {
        rg.AddForce(-m.forward * 1000f, ForceMode.Acceleration);
        rg.useGravity = true;
        statement = true;
        Debug.Log("Door");
    }
  
}
