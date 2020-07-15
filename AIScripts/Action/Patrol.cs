using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class Patrol : Action
{
    public NavMeshAgent Agent;
    public Transform[] NowPositions;
    public int NowPoint = 1;
    public float AllowDistance = 0.1f;
    public Transform Target;

    public override void OnStart()
    {
        Agent = gameObject.GetComponent<NavMeshAgent>();
        NowPositions = GetComponent<Mother>().Positions;
    }

    public override TaskStatus OnUpdate()
    {
        GetComponent<Animator>().SetBool("isWalk", true);
        Agent.isStopped = false;

        Target = NowPositions[NowPoint];
        Agent.SetDestination(Target.position);
        if (Vector3.Distance(transform.position, Target.position) <= AllowDistance)
        {
            NowPoint = (NowPoint + 1) % NowPositions.Length;
            if (NowPoint == 0)
            {
                NowPoint++;
            }
        }
        return TaskStatus.Running;
    }
}
