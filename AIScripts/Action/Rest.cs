using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine.AI;

public class Rest : Action 
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Mother mother;
    public override void OnStart()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        animator.SetBool("isWalk", false);
        navMeshAgent.isStopped = true;
        mother.ChangeTempTensionValue(-mother.TempTension);
        return TaskStatus.Success;
    }
}
