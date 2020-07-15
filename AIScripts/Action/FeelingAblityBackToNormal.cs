using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FeelingAblityBackToNormal : Action
{
    public SharedFloat hearRange;
    public SharedFloat SightRange;
    public SharedGameObject Mother;
    //可以降低至正常试听范围的阈值
    public int IsCanBackToNormalTension;
    public float DefaultHearRange=5;
    public float DefaultSightRange=5;

    public override TaskStatus OnUpdate()
    {
        if(Mother.Value.GetComponent<Mother>().WholeTension<=IsCanBackToNormalTension)
        {
            hearRange.Value = DefaultHearRange;
            SightRange.Value = DefaultSightRange;
        }
        
        return TaskStatus.Success;
    }
}
