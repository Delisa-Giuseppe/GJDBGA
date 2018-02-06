using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour { 

    //Stat
    public int DamageOutput;
    public int Defence;
    public float PlayerSpeed;

    void Start () {
		
	}
	
	
    void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    { 
       //if(transform.parent.gameObject.GetComponent<PlayerStat>().isArmed)
        //Debug.Log(other.name+" "+this.gameObject.name);
    }
}
