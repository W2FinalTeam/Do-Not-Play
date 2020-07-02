using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanCatchPlayer : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("抓捕距离")]
    public float distance;

    [BehaviorDesigner.Runtime.Tasks.Tooltip("玩家")]
    public SharedGameObject player;


    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(player.Value.transform.position,transform.position) > distance)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
