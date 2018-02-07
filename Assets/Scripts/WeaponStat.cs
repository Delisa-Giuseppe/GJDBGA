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

        if (pc && !player)
        {
            pc.DamageOutput = DamageOutput;
            pc.Defence = Defence;
            pc.PlayerSpeed = PlayerSpeed;
            player = pc;
            transform.parent = pc.weaponPosition;
            transform.position = pc.weaponPosition.position;
            pc.isArmed = true;
            pc.weapon = this;
            GetComponent<SphereCollider>().enabled = false;
        }
        else if (pc && pc.playerID != player.playerID && !pc.isDefending)
        {
            pc.anim.SetTrigger("TakeDamage");
            pc.CmdAnimate("TakeDamage", false, false);
            pc.Health -= player.DamageOutput;
            pc.CmdUpdateServer(pc.isAttacking, pc.isDefending, pc.Health);
            pc.labelPlayer.text = " HEALTH : " + pc.Health;
        }
    }

    //void ManageWeapon(Collider other)
    //{
    //    PlayerController pc = other.GetComponent<PlayerController>();
    //    if (player)
    //        Debug.Log(player.isAttacking);

    //    if (pc)
    //    {
    //        if (!player)
    //        {
    //            pc.DamageOutput = DamageOutput;
    //            pc.Defence = Defence;
    //            pc.PlayerSpeed = PlayerSpeed;
    //            player = pc;
    //            transform.parent = pc.weaponPosition;
    //            transform.position = pc.weaponPosition.position;
    //            pc.isArmed = true;
    //        }
    //        else if (pc.playerID != player.playerID && player.isAttacking)
    //        {
    //            if (other.gameObject.GetComponent<PlayerStat>() != null)
    //            {
    //                Debug.Log("Il guerriero " + player.playerID + " Ha colpito il guerriero" + pc.playerID);
    //                pc.Health -= player.DamageOutput;
    //                pc.labelPlayer.text = " HEALTH : " + pc.Health;
    //            }
    //        }
    //    }
    //}
}
