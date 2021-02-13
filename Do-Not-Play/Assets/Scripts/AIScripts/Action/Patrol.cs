using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Patrol : Action
{
    public NavMeshAgent Agent;
    private Mother mother;
    private Animator animator;
    public float AllowDistance = 0.5f;
    public Transform Target;
    float t = 0;

    public override void OnStart()
    {
        mother = GetComponent<Mother>();
        animator = GetComponent<Animator>();
        Agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        Agent.isStopped = false;

        Target = mother.Positions[mother.NowPoint];
        Agent.SetDestination(Target.position);
        if (Agent.remainingDistance <= AllowDistance && !Agent.pathPending)
        {
            t += Time.deltaTime;
            animator.SetBool("isWalk", false);
            if (t >= 1)
            {
                t = 0;
                mother.NowPoint = (mother.NowPoint + 1) % mother.Positions.Length;
                if (mother.NowPoint == 0)
                {
                    mother.NowPoint++;
                }
            }
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
        return TaskStatus.Success;
    }
}
