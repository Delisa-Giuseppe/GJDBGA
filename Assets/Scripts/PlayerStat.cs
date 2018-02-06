using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {

    //Stat
    public int Health;
    public int DamageOutput;
    public int Defence;
    public float PlayerSpeed;

    //Combat State
    private bool isAttacking;
    private bool isDefending;

    private bool isAlive;
    private bool isArmed;

    //Network State
    public bool isReady;

	// Use this for initialization
	void Start () {
        //Initialize State
        isAttacking = false;
        isArmed = false;
        isAlive = true;
        isDefending = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Health=0; GameOver;
        if(Health<=0)
        {
            isAlive = false;
        }
        playerState();
		if(Input.GetMouseButtonDown(0))//Tast Destro
        {
            //Debug.Log("Attacco");
        }
        if (Input.GetMouseButtonDown(1))//Tasto Sinistro
        {
          //  Debug.Log("Difeso");
        }
    }

    private void playerState()
    {
        if (!isAlive)//se il player non ha vita, Muore;
        {
            Destroy(this.gameObject);
        }
          
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.tag == "Weapon")
        {
            DamageOutput = other.GetComponent<WeaponStat>().DamageOutput;
            Defence = other.GetComponent<WeaponStat>().Defence;
            PlayerSpeed = other.GetComponent<WeaponStat>().PlayerSpeed;
            Destroy(other.gameObject);
        }
    }
}
