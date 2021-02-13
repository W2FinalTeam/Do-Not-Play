using UnityEngine.AI;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GotoHeardDestination : Action
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("移动的速度")]
    public float speed = 2;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("到达距离")]
    public float distance = 0.5f;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("无法到达时最大距离")]
    public float distanceReal = 5f;

    private float distancePre;
    private NavMeshAgent agent;
    private Animator animator;
    private Mother mother;

    public override void OnStart()
    {
        distancePre = distance;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        animator.SetBool("isWalk", true);
        agent.isStopped = false;
        agent.speed = speed;

        agent.SetDestination(mother.Destination);
        //修改无法到达时的距离
        distance = agent.hasPath ? distancePre : distanceReal;
        if (agent.remainingDistance > distance || agent.pathPending)
        {
            return TaskStatus.Running;
        }
        mother.HearTarget = null;
        mother.HearSomething = false;
        mother.Destination = Vector3.zero;
        return TaskStatus.Success;
    }
}
