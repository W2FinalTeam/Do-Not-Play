using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    public GameObject lostItem = null;
    public int StaticTension;
    public int TempTension;
    public int WholeTension;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        TempTension = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StaticTension = gameManager.Getvalue("紧张度");
    }
    public void ChangeStaticTensionValue(int RiseValue)
    {
        StaticTension += RiseValue;
        gameManager.Setvalue("紧张度", StaticTension);
    }
    public void ChangeTempTensionValue(int RiseValue)
    {
        TempTension += RiseValue;
    }
    // Update is called once per frame
    void Update()
    {
        WholeTension = StaticTension + TempTension;
    }
}
