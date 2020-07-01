using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PasswordLock : ChangeableItem
{

    public GameObject LockUI;
    public string passWord;
    private InputField inputField;
    private GameObject player;

    private bool isUnLock = false;
    //门锁UI的开关
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
        LockUI.transform.Find("ButtonExit").GetComponent<Button>().onClick.AddListener(UIExit);
        LockUI.transform.Find("ButtonSubmit").GetComponent<Button>().onClick.AddListener(
            delegate ()
            {
                OnEndEdit(inputField.text);
            });
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact()
    {

    }
    public override void Interact(GameObject inHandItem)
    {
        if (isUnLock)
        {
            anim.enabled = true;
            anim.SetBool(animBoolName, !anim.GetBool(animBoolName));
            return;
        }
        LockUI.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<Player>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }
    private void OnEndEdit(string s)
    {
        if (s == passWord)
        {
            isUnLock = true;
            UIExit();
        }
        else
        {
            inputField.text = "";
        }
    }
    private void UIExit()
    {
        LockUI.SetActive(false);
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponent<Player>().enabled = true;
    }
}
