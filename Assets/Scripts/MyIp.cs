using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class MyIp : NetworkBehaviour {
    [Header("Per ottenere IP entrare in playmode")]
    public string myIP;

   public Text IPText;
	// Use this for initialization
	void Start () {
      //  IPText = transform.Find("Text").GetComponent<Text>();
        myIP = "";
        if(IPText)
            IPText.text = myIP;

    }
	
	// Update is called once per frame
	void Update () {


        myIP = LocalIPAddress();
        if (NetworkManager.singleton.networkAddress == "localhost" && IPText)
        {
            IPText.gameObject.SetActive(true);
            IPText.text ="You are the host, this is your IP "+ System.Environment.NewLine + LocalIPAddress();          
        }
    }
    
    public void StartHASHost()
    {
        GetComponent<NetworkManager>().StartHost();
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
