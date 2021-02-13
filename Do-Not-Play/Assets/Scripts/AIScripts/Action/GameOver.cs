using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameOver : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("加载图片")]
    public GameObject loadImage;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("玩家")]

    public SharedGameObject player;

    private FirstPersonController FPC;
    public override void OnAwake()
    {
        FPC = player.Value.GetComponent<FirstPersonController>();
    }
    public override TaskStatus OnUpdate()
    {
        StartCoroutine("Load");
        player.Value.GetComponent<Player>().Restart();
        return TaskStatus.Success;
    }
    IEnumerator Load()
    {
        FPC.enabled = false;
        loadImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        loadImage.SetActive(false);
        FPC.enabled = true;
    }
}
