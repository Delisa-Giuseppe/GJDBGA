using DG.Tweening;
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
    [SyncVar] public bool isDefending;
    [SyncVar] public bool isAttacking;
    [SyncVar] public bool isArmed;
    [SyncVar] public int gamePoints = 0;
    [SyncVar] public int maxHealth;
    public Text labelPlayer;
    public GameObject playerPoint;
    public Transform weaponPosition;
    public GameObject shield;
    public GameObject helmet;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public WeaponStat weapon;
    public SkinnedMeshRenderer mesh;
    public List<AudioClip> soundsPlayer;

    [HideInInspector]
    public AudioSource audio;

    [Range (1, 10)]
    public int playerMovementSpeed;

    private LineRenderer lr;
    private Vector3 cursorPosition;
    private Vector3 playerDirection;
    private GameManager gm;
    [SyncVar] public bool isDead;
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>();
        gm.PlayerList.Add(this);
        isDefending = false;
        isAttacking = false;
        maxHealth = Health;

        if(isLocalPlayer)
        {
            PlayerPointEnable();
        }
    }

    void Update()
    {
        if(!isDead)
        {
            if (GameManager.startGame)
            {
                PlayerMovement();
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
    public void CmdUpdateServer(bool defence)
    {
        RpcUpdateServer(defence);
    }

    [ClientRpc]
    private void RpcUpdateServer(bool defence)
    {
        isDefending = defence;
    }

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
                audio.PlayOneShot(soundsPlayer[3], 1);
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

            //anim.SetTrigger("StartGame");

            //anim.SetBool("IsAttacking", isAttacking);
            //anim.SetBool("IsDefending", isDefending);

        }

        OnDefence();

        CmdAnimate("StartGame", false, true);

        CmdAnimate("IsAttacking", isAttacking, false);
        CmdAnimate("IsDefending", isDefending, false);

        CmdUpdateServer(isDefending);
        gm.healthBar[playerID - 1].GetComponent<Image>().fillAmount = (float)gm.playerList[playerID - 1].Health / (float)gm.playerList[playerID - 1].maxHealth;
        labelPlayer.transform.rotation = Quaternion.LookRotation(labelPlayer.transform.position - Camera.main.transform.position);
    }

    public void OnDefence()
    {
        if (isDefending)
        {
            shield.SetActive(true);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            //audio.PlayOneShot(soundsPlayer[2], 1);
        }
        else
        {
            shield.SetActive(false);
        }
    }

    public void InitAttack()
    {
        weapon.gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    public void EndAttack()
    {
        weapon.gameObject.GetComponent<MeshCollider>().enabled = false;
    }

    public void TakeDamage(PlayerController pc, int damage)
    {
        int id = pc.playerID - 1;
        gm.playerList[id].Health -= damage;
        gm.playerList[id].anim.SetTrigger("TakeDamage");

        if (gm.playerList[id].Health <= 0 && !gm.playerList[id].isDead)
        {
            gm.playerList[id].isDead = true;
            gm.playerList[id].Health = 0;
            //gm.playerList[id].anim.SetTrigger("Death");
            gm.playerList[id].audio.PlayOneShot(soundsPlayer[1], 1);
            gm.playerList[id].CmdAnimate("Death", false, true);
            gm.playerList[id].PlayerPointDisable();
            gm.matchDeathCounter++;
            gm.checkVictory();
        }
    }

    public bool hitMagma;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Magma" && !this.isDead && this.Health > 0 && !hitMagma)
        {
            print("MAGMA");
            this.hitMagma = true;
            this.Health = 0;
            this.isDead = true;
            this.audio.PlayOneShot(soundsPlayer[1], 1);
            this.CmdAnimate("Death", false, true);
            this.PlayerPointDisable();
            gm.matchDeathCounter++;
            gm.checkVictory();
        }
    }

    [Command]
    private void CmdMagma()
    {
        RpcMagma();
    }

    [ClientRpc]
    private void RpcMagma()
    {
        
    }
}
