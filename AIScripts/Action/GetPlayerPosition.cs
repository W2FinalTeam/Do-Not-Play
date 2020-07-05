using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetPlayerPosition : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("玩家坐标")]
    public SharedTransform position;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("玩家")]
    public SharedGameObject player;
    public override TaskStatus OnUpdate()
    {
        position = player.Value.transform;
        return TaskStatus.Success;
    }
}
