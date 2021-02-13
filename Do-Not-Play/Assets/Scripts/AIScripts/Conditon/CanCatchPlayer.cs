using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityStandardAssets.Characters.FirstPerson;

public class CanCatchPlayer : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("抓捕距离")]
    public float distance;

    [BehaviorDesigner.Runtime.Tasks.Tooltip("玩家")]
    public SharedGameObject player;

    private Vector3 m_position;
    private Vector3 p_position;

    public override TaskStatus OnUpdate()
    {
        m_position = transform.position;
        p_position = player.Value.transform.position;
        m_position.y = 0;
        p_position.y = 0;
        if (Vector3.Distance(p_position, m_position) > distance)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
