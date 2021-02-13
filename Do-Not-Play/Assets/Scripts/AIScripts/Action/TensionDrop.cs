using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class TensionDrop : Action 
{
    public int ChangeValuePerSec = -1;
    private Mother mother;
    public override void OnAwake()
    {
        mother = GetComponent<Mother>();
    }
    public override TaskStatus OnUpdate()
    {
        mother.ChangeTempTensionValue(ChangeValuePerSec * Time.deltaTime);
        return TaskStatus.Success;
    }
}
