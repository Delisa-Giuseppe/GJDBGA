using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    [SyncVar] public int playerID;
    [SyncVar] public int Health;
    [SyncVar] public int DamageOutput;
    [SyncVar] public int Defence;
    [SyncVar] public float PlayerSpeed;
    [SyncVar] public bool isDefending;
    [SyncVar] public bool isAttacking;
    [SyncVar] public bool isArmed;
    public Text labelPlayer;
    public GameObject playerPoint;
    public Transform weaponPosition;
    public GameObject shield;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public WeaponStat weapon;

    [Range (1, 10)]
    public int playerMovementSpeed;

    private LineRenderer lr;
    private Vector3 cursorPosition;
    private Vector3 playerDirection;
    private GameManager gm;
    private bool start;
    private bool isDead;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>();
        gm.PlayerList.Add(this);
        start = false;
        IsDefending = false;

        if(isLocalPlayer)
            PlayerPointEnable();
    }

    void Update()
    {
        if(!isDead)
        {
            if (GameManager.startGame)
            {
                PlayerMovement();
            }

            if (Health <= 0)
            {
                isDead = true;
                anim.SetTrigger("Death");
                PlayerPointDisable();
            }
        }
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    CmdChangeSkin(skinIndex, gameObject);
        //}
    }

    [Client]
    void PlayerPointEnable()
    {
        playerPoint.SetActive(true);
    }

    [Client]
    void PlayerPointDisable()
    {
        playerPoint.SetActive(false);
    }

    [Command]
    void CmdUpdate()
    {
        RpcUpdateClient();
    }

    [ClientRpc]
    void RpcUpdateClient()
    {
        if (isLocalPlayer)
            return;

        labelPlayer.transform.rotation = Quaternion.LookRotation(labelPlayer.transform.position - Camera.main.transform.position);

        if (!start)
        {
            anim.SetTrigger("StartGame");
            start = true;
        }
    }
    
    [Command]
    public void CmdDefence()
    {
        if (IsDefending)
        {
            shield.SetActive(true);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            shield.SetActive(false);
        }
    }

    //[Command]
    //void CmdChangeSkin(int skinIndex, GameObject player)
    //{
    //    if (skinIndex == 0)
    //    {
    //        skinIndex = 1;
    //    }
    //    else if (skinIndex == 1)
    //    {
    //        skinIndex = 0;
    //    }

    //    RpcChangeSkin(skinIndex, player);

    //}

    //[ClientRpc]
    //void RpcChangeSkin(int skinIndex, GameObject player)
    //{
    //    if (skinIndex == 0)
    //    {
    //        player.transform.GetChild(1).gameObject.SetActive(false);
    //        player.transform.GetChild(0).gameObject.SetActive(true);
    //    }
    //    else if (skinIndex == 1)
    //    {
    //        player.transform.GetChild(0).gameObject.SetActive(false);
    //        player.transform.GetChild(1).gameObject.SetActive(true);
    //    }
    //}

    void PlayerMovement ()
    {
        if (isLocalPlayer)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)/* && hit.transform.tag == "Terrain"*/)
            {
                cursorPosition = hit.point;
                cursorPosition.y = transform.position.y;
                playerDirection = (cursorPosition - transform.position).normalized;
                if (Vector3.Distance(cursorPosition, transform.position) > 1)
                {
                    transform.DOLookAt(cursorPosition, 0.5f);
                    GetComponent<Rigidbody>().velocity = playerDirection * playerMovementSpeed;
                }

                cursorPosition.y += GetComponent<CapsuleCollider>().height / 2;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, cursorPosition);
            }

            if (Input.GetMouseButtonDown(0) && !IsDefending && isArmed)//Tast Destro
            {
                isAttacking = true;
                Debug.Log("Attacco");
            }
            else
            {
                isAttacking = false;
            }

            if (Input.GetMouseButton(1) && !isAttacking)//Tasto Sinistro
            {
                IsDefending = true;
                Debug.Log("Difeso");
            }
            else
            {
                IsDefending = false;
            }

            labelPlayer.transform.rotation = Quaternion.LookRotation(labelPlayer.transform.position - Camera.main.transform.position);
            CmdDefence();

            if (!start)
            {
                anim.SetTrigger("StartGame");
                start = true;
            }

            anim.SetBool("IsAttacking", isAttacking);
            anim.SetBool("IsDefending", IsDefending);
        }
        else
        {
            return;
        }

        CmdDefence();
        CmdUpdate();
    }

    public void InitAttack()
    {
        weapon.GetComponent<SphereCollider>().enabled = true;
    }

    public void EndAttack()
    {
        weapon.GetComponent<SphereCollider>().enabled = false;
    }

    public bool IsDefending
    {
        get
        {
            return isDefending;
        }

        set
        {
            isDefending = value;
        }
    }
}
