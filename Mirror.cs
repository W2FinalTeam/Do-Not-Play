using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Mirror : ChangeableItem //镜子
{
    public GameObject detergent;//洗洁精
    private bool isinHand = false;
    public Player player;
    public GameObject text;//3Dtext
    public GameObject canvas;//提示信息
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.targetItem == this.gameObject && text.active == false)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }
    public override void Interact()
    {

    }
    public override void Interact(GameObject inHandItem)
    {
        if (inHandItem == detergent) //洗洁精在手上
        {
            //出现字体
            text.SetActive(true);
        }
    }
}
