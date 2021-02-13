using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private GameObject mother;
    private AudioSource aduio;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        mother = GameObject.Find("Mother");
        aduio = GetComponent<AudioSource>();
        //aduio.clip = ((AudioClip)Resources.Load("心跳声"));
        aduio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, mother.transform.position);
        if (distance <= 1)
        {
            aduio.volume = 1f;
        }
        else
        {
            aduio.volume = 1f - (distance - 1) / 15;
        }

    }
}
