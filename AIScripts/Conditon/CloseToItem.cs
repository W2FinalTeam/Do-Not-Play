using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class CloseToItem : Conditional 
{
    [BehaviorDesigner.Runtime.Tasks.Tooltip("和物品距离")]
    public float distance;
    [BehaviorDesigner.Runtime.Tasks.Tooltip("丢失的物品")]
    public SharedGameObject lostItem;
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(lostItem.Value.transform.position, transform.position) > distance)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
