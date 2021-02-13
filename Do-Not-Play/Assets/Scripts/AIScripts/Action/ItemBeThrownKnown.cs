using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class ItemBeThrownKnown : Action 
{
    private Mother mother;
    public override void OnAwake()
    {
        mother = GetComponent<Mother>();
    }
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        mother.FindItem.GetComponent<KeyItem>().IsChecked2 = true;
        mother.FindItem.GetComponent<MoveableItem>().Init();
        mother.FindItem = null;
        return TaskStatus.Success;
    }
}
