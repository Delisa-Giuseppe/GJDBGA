using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCanvas : MonoBehaviour {

    Text labelPlayer;

	// Use this for initialization
	void Start () {
        labelPlayer = transform.GetChild(0).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
