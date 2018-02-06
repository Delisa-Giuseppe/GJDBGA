using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    [SyncVar] public int playerID;
    [SyncVar] public bool isDefending;
    public Text labelPlayer;
    public GameObject playerPoint;

    private PlayerStat ps;
    private LineRenderer lr;
    private Vector3 cursorPosition;
    private Vector3 playerDirection;
    private GameManager gm;
    private Animator anim;
    private bool start;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        ps = GetComponent<PlayerStat>();
        lr = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>();
        gm.PlayerList.Add(this);
        start = false;
        IsDefending = false;

        if(isLocalPlayer)
            PlayerPoint();
    }

    void Update()
    {
        if(GameManager.startGame)
        {
            PlayerMovement();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 500);
        }

        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    CmdChangeSkin(skinIndex, gameObject);
        //}
    }

    [Client]
    void PlayerPoint()
    {
        playerPoint.SetActive(true);
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
        ps.OnDefence();

        if (!start)
        {
            anim.SetTrigger("StartGame");
            start = true;
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

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Terrain")
            {
                cursorPosition = hit.point;
                cursorPosition.y += GetComponent<CapsuleCollider>().height / 2;
                playerDirection = (cursorPosition - transform.position).normalized;
                GetComponent<Rigidbody>().AddForce(playerDirection * (0.1f + (ps.PlayerSpeed * 0.03f)), ForceMode.VelocityChange);
                transform.DOLookAt(cursorPosition, 0.5f);

                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, cursorPosition);
            }

            if (Input.GetMouseButtonDown(0) && !IsDefending)//Tast Destro
            {
                ps.IsAttacking = true;
                Debug.Log("Attacco");
            }
            if (Input.GetMouseButtonUp(0))
            {
                ps.IsAttacking = false;
            }
            if (Input.GetMouseButtonDown(1) && !ps.IsAttacking)//Tasto Sinistro
            {
                IsDefending = true;
                Debug.Log("Difeso");
            }
            if (Input.GetMouseButtonUp(1))//Tasto Sinistro
            {
                IsDefending = false;
            }

            labelPlayer.transform.rotation = Quaternion.LookRotation(labelPlayer.transform.position - Camera.main.transform.position);
            ps.OnDefence();

            if (!start)
            {
                anim.SetTrigger("StartGame");
                start = true;
            }
        }
        else
        {
            return;
        }

        CmdUpdate();
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
