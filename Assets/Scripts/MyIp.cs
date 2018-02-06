using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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
        myIP = LocalIPAddress();

    }
    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
