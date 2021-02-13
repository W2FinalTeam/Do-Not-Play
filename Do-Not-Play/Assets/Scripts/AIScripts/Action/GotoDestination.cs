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

    float t;
    public override void OnStart()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        agent.isStopped = false;
        agent.speed = speed;
        if (mother.Destination == Vector3.zero)
            return TaskStatus.Failure;
        agent.SetDestination(mother.Destination);

        if (agent.remainingDistance > distance || agent.pathPending)
        {
            animator.SetBool("isWalk", true);
            return TaskStatus.Running;
        }
        else if (agent.hasPath)
        {
            t += Time.deltaTime;
            animator.SetBool("isWalk", false);
            if (t >= 1.5)
            {
                t = 0;
                mother.Destination = Vector3.zero;
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
        mother.Destination = Vector3.zero;
        return TaskStatus.Failure;
    }
}
