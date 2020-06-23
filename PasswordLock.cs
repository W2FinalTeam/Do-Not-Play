using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PasswordLock : ChangeableItem
{
    public GameObject LockUI;
    public string passWord;
    private string mine;
    private InputField inputField;
    private bool isUnLock = false;
    private bool isOpen = false;

    private Animator anim;

    private const string animBoolName = "isOpen_Obj_1";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;   
        inputField =LockUI.GetComponentInChildren<InputField>(true);
        inputField.onEndEdit.AddListener(OnEndEdit);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact()
    {
        if (isUnLock)
        {
            bool isOpen = anim.GetBool(animBoolName);

            anim.enabled = true;
            anim.SetBool(animBoolName, !isOpen);
            return;
        }
        LockUI.SetActive(!isOpen);
    }
    public void OnEndEdit(string s)
    {
        if (s == passWord)
            isUnLock = true;
    }

}
