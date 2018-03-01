using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour { 

    //Stat
    public int DamageOutput;
    public int Defence;
    public float PlayerSpeed;
    public PlayerController player;

    bool hit;

    void Start () {
        player = null;
	}
	
	
    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();

        if (pc && !pc.isArmed && !player)
        {
            pc.DamageOutput = DamageOutput;
            pc.Defence = Defence;
            pc.PlayerSpeed = PlayerSpeed;
            pc.maxHealth = Defence * pc.maxHealth;
            pc.Health = pc.maxHealth;
            player = pc;
            transform.parent = pc.weaponPosition;
            transform.position = pc.weaponPosition.position;
            pc.isArmed = true;
            pc.weapon = this;
            GetComponent<MeshCollider>().enabled = false;
        }
        else if (pc && pc != player && !pc.isDefending)
        {
<<<<<<< HEAD
            Debug.Log("Il guerriero " + player.playerID + " Ha colpito il guerriero" + pc.playerID);
            pc.anim.SetTrigger("TakeDamage");
            pc.Health -= player.DamageOutput;
            pc.labelPlayer.text = " HEALTH : " + pc.Health;
=======
            pc.TakeDamage(pc, player.DamageOutput);
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
        }
    }
}
