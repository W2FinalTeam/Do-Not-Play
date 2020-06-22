using UnityEngine;
using System.Collections;

public class HidingPoint : ChangeableItem
{
	
	private Animator anim;

	private const string animBoolName = "isOpen_Obj_1";

	void Start()
	{
		anim = GetComponent<Animator>(); 
		anim.enabled = false;  
	}
	void Update()
	{
	}
    public override void Interact()
    {
		bool isOpen = anim.GetBool(animBoolName);    

		anim.enabled = true;
		anim.SetBool(animBoolName, !isOpen);
	}
}
