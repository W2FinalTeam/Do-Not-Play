using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GotoDestination : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("移动的速度")]
    public float speed = 2;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("坐标")]
    public SharedTransform position;
    public override TaskStatus OnUpdate()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<NavMeshAgent>().speed = speed;
        GetComponent<NavMeshAgent>().SetDestination(position.Value.position);
        return TaskStatus.Success;
    }
}
