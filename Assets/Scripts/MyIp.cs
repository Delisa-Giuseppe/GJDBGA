using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class MyIp : MonoBehaviour {
    [Header("Per ottenere IP entrare in playmode")]
    public string myIP;
	// Use this for initialization
	void Start () {
        myIP = "";
	}
	
	// Update is called once per frame
	void Update () {
        myIP = NetworkManager.singleton.networkAddress;

    }
}
