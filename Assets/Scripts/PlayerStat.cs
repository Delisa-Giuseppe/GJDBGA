using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{

    //Stat
    public int Health;
    public int DamageOutput;
    public int Defence;
    public float PlayerSpeed;

    //Combat State
    [SerializeField] private bool isAttacking;

    private bool isAlive;
    public bool isArmed;
    private PlayerController pc;

    //Network State
    public bool isReady;

    public MeshRenderer ShieldRenderer;

    public BoxCollider ShieldCollider;

    // Use this for initialization
    void Start()
    {
        //Initialize State
        pc = GetComponent<PlayerController>();
        IsAttacking = false;
        isArmed = false;
        isAlive = true;
        OnDefence();
    }

    // Update is called once per frame
    void Update()
    {

        //Health=0; GameOver;
        if (Health <= 0)
        {
            isAlive = false;
        }
        playerState();
        
    }

    private void playerState()
    {
        if (!isAlive)//se il player non ha vita, Muore;
        {
            Destroy(this.gameObject);
        }

    }

    public void OnDefence()
    {
        if (pc.IsDefending)
        {
            ShieldCollider.enabled = true;
            ShieldRenderer.enabled = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            ShieldCollider.enabled = false;
            ShieldRenderer.enabled = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Terrain")
        {
            if(!pc.IsDefending)
            {
                if (other.tag == "Weapon" && !isArmed)
                {
                    DamageOutput = other.GetComponent<WeaponStat>().DamageOutput;
                    Defence = other.GetComponent<WeaponStat>().Defence;
                    PlayerSpeed = other.GetComponent<WeaponStat>().PlayerSpeed;
                    other.transform.parent = this.gameObject.transform;
                    isArmed = true;
                }
                if (isArmed && IsAttacking)
                {
                    if (pc.playerID!= other.gameObject.GetComponent<PlayerController>().playerID)
                    {
                        if (other.gameObject.GetComponent<PlayerStat>() != null)
                        {
                            Debug.Log("Il guerriero "+this.pc.playerID + " Ha colpito il guerriero" +other.gameObject.GetComponent<PlayerController>().playerID);
                            other.GetComponent<PlayerStat>().Health -= other.GetComponent<PlayerStat>().DamageOutput;
                        }
                    }
                }
            }
        }
    }

    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }

        set
        {
            isAttacking = value;
        }
    }
}
