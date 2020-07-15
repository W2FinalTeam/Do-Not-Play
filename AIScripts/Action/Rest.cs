using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Rest : Action 
{
    public float TempRiseValue = 0.1f;
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
        animator.SetBool("isRun", false);
        navMeshAgent.isStopped = true;
        if (!isRunning.Value)
        {
            mother.ChangeTempTensionValue(-mother.TempTension);
            isRunning.Value = true;
        }        
        else
        {
            mother.ChangeTempTensionValue(TempRiseValue);
        }
        return TaskStatus.Running;
    }
}
