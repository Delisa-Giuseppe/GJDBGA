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
<<<<<<< HEAD
    private bool start;
    private bool isDead;
=======
    [SyncVar] public bool isDead;
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>();
        gm.PlayerList.Add(this);
<<<<<<< HEAD
        start = false;
        IsDefending = false;
=======
        isDefending = false;
        isAttacking = false;
        maxHealth = Health;
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016

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
<<<<<<< HEAD

            if (Health <= 0)
            {
                isDead = true;
                anim.SetTrigger("Death");
                PlayerPointDisable();
            }
=======
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
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
<<<<<<< HEAD
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

=======
    public void CmdUpdateServer(bool defence, int score)
    {
        RpcUpdateServer(defence, score);
    }

    [ClientRpc]
    private void RpcUpdateServer(bool defence, int score)
    {
        isDefending = defence;
        gamePoints = score;
    }

>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
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
<<<<<<< HEAD
                Debug.Log("Attacco");
=======
                audio.PlayOneShot(soundsPlayer[3], 1);
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
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

<<<<<<< HEAD
            labelPlayer.transform.rotation = Quaternion.LookRotation(labelPlayer.transform.position - Camera.main.transform.position);
            CmdDefence();

            if (!start)
            {
                anim.SetTrigger("StartGame");
                start = true;
            }

            anim.SetBool("IsAttacking", isAttacking);
            anim.SetBool("IsDefending", IsDefending);
=======
            //anim.SetTrigger("StartGame");

            //anim.SetBool("IsAttacking", isAttacking);
            //anim.SetBool("IsDefending", isDefending);

        }

        OnDefence();

        CmdAnimate("StartGame", false, true);

        CmdAnimate("IsAttacking", isAttacking, false);
        CmdAnimate("IsDefending", isDefending, false);

        CmdUpdateServer(isDefending, gamePoints);
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
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Magma" && !this.isDead)
        {
            this.Health = 0;
            this.isDead = true;
            this.audio.PlayOneShot(soundsPlayer[1], 1);
            this.CmdAnimate("Death", false, true);
            this.PlayerPointDisable();
            gm.matchDeathCounter++;
            gm.checkVictory();
        }
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
