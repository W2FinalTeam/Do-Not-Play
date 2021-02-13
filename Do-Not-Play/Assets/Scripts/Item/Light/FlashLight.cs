using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashLight : Tool
{

    GameManager gameManager;
    public GameObject FlashLightPostition;
    public FlashLightController controller;
    MeshRenderer meshRenderer;
    BoxCollider boxCollider;
    public Light Flash;
    //在工具中打开或者按f使用手电筒
    public override void Use()
    {
        this.transform.position = FlashLightPostition.transform.position;
        this.transform.rotation = FlashLightPostition.transform.rotation;
        this.transform.SetParent(FlashLightPostition.transform);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        Flash.enabled = true;
        controller.enabled = true;
    }
    //再按一次f关闭手电筒
    public override void UnUse()
    {
        Flash.enabled = false;
    }
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Flash = transform.Find("Spot Light").GetComponent<Light>();
        FlashLightPostition = GameObject.Find("FlashLightPosition");
        controller = gameObject.GetComponent<FlashLightController>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public override GameObject PickUpItem(Transform parent)
    {
        this.transform.position = new Vector3(100, 0, 0);
        Flash.enabled = false;
        controller.enabled = true;
        return this.gameObject;
    }
   
}
