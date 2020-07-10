using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

public class GameOver : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("加载图片")]
    public GameObject loadImage;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("玩家")]
    public SharedGameObject player;
    public override TaskStatus OnUpdate()
    {
        StartCoroutine("Load");
        player.Value.GetComponent<Player>().Restart();
        return TaskStatus.Success;
    }
    IEnumerator Load()
    {
        loadImage.SetActive(true);
        yield return new WaitForSeconds(3f);
        loadImage.SetActive(false);
    }
}
