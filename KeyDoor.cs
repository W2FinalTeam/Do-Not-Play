using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : ChangeableItem
{
    public GameObject axe;//斧头
    public Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact(GameObject inHandItem)
    {
        if (inHandItem == axe)
        {
            this.GetComponent<Animator>().SetBool("isOpen_Obj_1", true);
        }
    }
    public override void Interact()
    {

    }
}
