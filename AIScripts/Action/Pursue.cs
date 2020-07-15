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
    }
    public override TaskStatus OnUpdate()
    {
        if (agent.SetDestination(playerTransform.position))
        {
            if (Vector3.Distance(transform.position, playerTransform.position) > distance)
            {
                return TaskStatus.Running;
            }
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
