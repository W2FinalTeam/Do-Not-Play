using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameManager gameManager;
    Player player;
    Mother mother;
    List<Tool> needCheck = new List<Tool>();
    private Timer timer;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = gameManager.p;
        mother = gameManager.m;
    }
    private void OnTriggerEnter(Collider other)
    {
        needCheck.Clear();
        if (other.CompareTag("Player"))
        {
            foreach (var t in gameManager.Tools)
                if (t.Value != null)
                {
                    needCheck.Add(t.Value.GetComponent<Tool>());
                }

            foreach (var t in needCheck)
            {
                if (gameManager.LevelManager.CheckTask(t.task))
                {
                    mother.GetCurrentWaypoints(gameManager.LevelManager.level);
                    timer = t.gameObject.GetComponent<Timer>();
                    if (timer != null)
                    {
                        timer.enabled = false;
                    }
                }
            }
        }
        player.gameObject.transform.position = player.room.transform.position;
        gameManager.Save();
    }
}
