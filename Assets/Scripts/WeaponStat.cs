using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour { 

    //Stat
    public int DamageOutput;
    public int Defence;
    public float PlayerSpeed;
    public PlayerController player;

    void Start () {
        player = null;
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
            player = pc;
            transform.parent = pc.weaponPosition;
            transform.position = pc.weaponPosition.position;
            pc.isArmed = true;
            pc.weapon = this;
            GetComponent<MeshCollider>().enabled = false;
        }
        else if (pc && pc != player && !pc.isDefending)
        {
            pc.TakeDamage(pc, player.DamageOutput);
        }
    }
}
