using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System.Collections.Specialized;
using System.Security.Cryptography;

public class GotoDestination : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("移动的速度")]
    public float speed = 2;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("坐标")]
    public SharedTransform position;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("到达距离")]
    public float distance = 0.5f;
    public override TaskStatus OnUpdate()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<NavMeshAgent>().speed = speed;
        GetComponent<NavMeshAgent>().SetDestination(position.Value.position);
        if(Vector3.Distance(transform.position, position.Value.position) > distance)
        {
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }
}
