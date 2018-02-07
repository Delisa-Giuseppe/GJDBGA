using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{

    //Stat


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
        //if (Health <= 0)
        //{
        //    isAlive = false;
        //}
        //PlayerState();
        
    }

    private void PlayerState()
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
