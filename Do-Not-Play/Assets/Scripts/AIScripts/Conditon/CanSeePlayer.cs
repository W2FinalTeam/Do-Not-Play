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
    Mother mother;
    public override void OnStart()
    {
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        Vector3 Mpoi = transform.position;
        Vector3 Ppoi = player.Value.transform.position;
        Mpoi.y = 1.5f;
        Ppoi.y = 0.8f;
        float distance = Vector3.Distance(Mpoi, Ppoi);
        float angle = Vector3.Angle(transform.forward, Ppoi - Mpoi);
        if (distance <= viewDistance && angle <= viewAngle * 0.5f)
        {
            Ppoi.y = 0.6f;
            RaycastHit hit;
            if (Physics.Raycast(Mpoi, Ppoi - Mpoi, out hit, viewDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    mother.Destination = player.Value.transform.position;
                    return TaskStatus.Success;
                }
            }
        }
        return TaskStatus.Failure;
    }
}



