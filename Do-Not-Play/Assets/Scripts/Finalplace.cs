using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
public class Finalplace : MonoBehaviour
{
    GameManager gameManager;
    PlayableDirector director;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        director =GetComponentInChildren<PlayableDirector>(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.LevelManager.CheckTask("逃离"))
            {
                FindObjectOfType<BehaviorTree>().DisableBehavior();
                gameManager.mother.GetComponent<NavMeshAgent>().enabled = false;
                gameManager.p.enabled = false;
                gameManager.p.FPC.enabled = false;

                gameManager.UIManager.UImain["Tab"].UI.GetComponent<TabUI>().UseTool("手电筒");

                director.enabled = true;
                director.stopped += OnPlayableDirectorStopped;
            }
        }
    }
    /// <summary>
    /// 结束动画时
    /// </summary>
    /// <param name="aDirector"></param>
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        UIManager.GetInstance().SetUI("菜单", false);
        UIManager.GetInstance().SetUI("制作团队", true);
        Invoke("Quit",10f);
    }
    void Quit()
    {
        Application.Quit();
    }
}
