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
    private Player p;
    private bool isUnLock = false;

    private Animator anim;
    private const string animBoolName = "isOpen_Obj_1";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;

        inputField = LockUI.GetComponentInChildren<InputField>(true);
        inputField.onEndEdit.AddListener(OnEndEdit);
        LockUI.transform.Find("ButtonExit").GetComponent<Button>().onClick.AddListener(UIExit);
        LockUI.transform.Find("ButtonSubmit").GetComponent<Button>().onClick.AddListener(
            delegate ()
            {
                OnEndEdit(inputField.text);
            });
        player = GameObject.FindGameObjectWithTag("Player");
        p = player.GetComponent<Player>();
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
        p.FPC.enabled = false;
        p.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnEndEdit(string s)
    {
        if (s == passWord)
        {
            isUnLock = true;
            UIExit();
            Interact(null);
        }
        else
        {
            inputField.text = "";
        }
    }
    private void UIExit()
    {
        LockUI.SetActive(false);
        p.FPC.enabled = true;
        p.enabled = true;
    }
}
