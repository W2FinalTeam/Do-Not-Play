using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Rest : Action
{
    public float TempRiseValue = 5f;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Mother mother;
    public SharedBool isRunning;

    public override void OnStart()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        animator.SetBool("isWalk", false);
        if (navMeshAgent != null)
            navMeshAgent.isStopped = true;
        mother.ChangeTempTensionValue(TempRiseValue * Time.deltaTime);
        if (mother.TempTension >= 50)
        {
            isRunning.Value = true;
            navMeshAgent.isStopped = false;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
