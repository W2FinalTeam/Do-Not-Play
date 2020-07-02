using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class CanSeePlayer : Conditional
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("视线最大距离")]
    public float viewDistance;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("视野最大角度")]
    public float viewAngle;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("玩家")]
    public SharedGameObject player;

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, player.Value.transform.position) < viewDistance)
        {
            if (Vector3.Angle(transform.forward, player.Value.transform.position) < viewAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(player.Value.transform.position, transform.position - player.Value.transform.position, out hit, viewDistance)){
                    if (hit.collider.gameObject == player.Value)
                        return TaskStatus.Success;
                }
            }
        }
        return TaskStatus.Failure;
    } 
}
