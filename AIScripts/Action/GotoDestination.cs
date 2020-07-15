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
    [BehaviorDesigner.Runtime.Tasks.Tooltip("到达距离")]
    public float distance = 0.5f;

    private NavMeshAgent agent;
    private Animator animator;
    private Mother mother;

    public override void OnStart()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        animator.SetBool("isWalk", true);
        agent.isStopped = false;
        agent.speed = speed;
        if (mother.Destination == Vector3.zero)
            return TaskStatus.Failure;
        if (agent.SetDestination(mother.Destination)&&agent.hasPath)
        {
            if (Vector3.Distance(transform.position, mother.Destination) > distance)
            {
                return TaskStatus.Running;
            }
            mother.Destination = Vector3.zero;
            return TaskStatus.Success;
        }
        mother.Destination = Vector3.zero;
        return TaskStatus.Failure;
    }
}
