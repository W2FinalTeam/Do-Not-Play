using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Pursue : Action
{
    public SharedGameObject player;
    public float distance;
    private NavMeshAgent agent;
    private Transform playerTransform;
    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = player.Value.transform;
        GetComponent<Animator>().SetBool("isRun", true);
        agent.speed = 3;
    }
    public override TaskStatus OnUpdate()
    {
        agent.SetDestination(playerTransform.position);

        if (agent.remainingDistance > distance || agent.pathPending)
        {
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }
}
