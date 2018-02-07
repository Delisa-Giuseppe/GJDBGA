﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class PlayerController : NetworkBehaviour
{
    [SyncVar] public int playerID;
    [SyncVar] public int Health;
    [SyncVar] public int DamageOutput;
    [SyncVar] public int Defence;
    [SyncVar] public float PlayerSpeed;
    [SyncVar(hook = "OnDefence")] public bool isDefending;
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
    private bool isDead;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>();
        gm.PlayerList.Add(this);
        isDefending = false;
        isAttacking = false;

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
                CmdAnimate("Death", false, false);
                PlayerPointDisable();
            }
        }
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
    public void CmdAnimate(string name, bool value, bool trigger)
    {
        RpcAnimate(name, value, trigger);
    }

    [ClientRpc]
    void RpcAnimate(string name, bool value, bool trigger)
    {
        if(anim)
        {
            if (!trigger)
                anim.SetBool(name, value);
            else
                anim.SetTrigger(name);
        }
    }

    [Command]
    public void CmdUpdateServer(bool fight, bool defence, int health)
    {
        RpcUpdateServer(fight, defence, health);
    }

    [ClientRpc]
    private void RpcUpdateServer(bool fight, bool defence, int health)
    {
        isAttacking = fight;
        isDefending = defence;
        Health = health;
    }

    //[ClientRpc]
    //void RpcDefence()
    //{
    //    OnDefence();
    //}
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
                    if(!isDefending)
                        GetComponent<Rigidbody>().velocity = playerDirection * playerMovementSpeed;
                }

                cursorPosition.y += GetComponent<CapsuleCollider>().height / 2;
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, cursorPosition);
            }

            if (Input.GetMouseButtonDown(0) && !isDefending && isArmed)//Tasto Sinistro
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }

            if (Input.GetMouseButton(1) && !isAttacking)//Tast Destro
            {
                isDefending = true;
            }
            else
            {
                isDefending = false;
            }

            anim.SetTrigger("StartGame");
            CmdAnimate("StartGame", false, true);

            anim.SetBool("IsAttacking", isAttacking);
            anim.SetBool("IsDefending", isDefending);

            CmdAnimate("IsAttacking", isAttacking, false);
            CmdAnimate("IsDefending", isDefending, false);

            CmdUpdateServer(isAttacking, isDefending, Health);
        }

        labelPlayer.transform.rotation = Quaternion.LookRotation(labelPlayer.transform.position - Camera.main.transform.position);
    }

    public void OnDefence(bool defence)
    {
        if (defence)
        {
            shield.SetActive(true);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            shield.SetActive(false);
        }
    }

    public void InitAttack()
    {
        weapon.GetComponent<SphereCollider>().enabled = true;
    }

    public void EndAttack()
    {
        weapon.GetComponent<SphereCollider>().enabled = false;
    }
}
