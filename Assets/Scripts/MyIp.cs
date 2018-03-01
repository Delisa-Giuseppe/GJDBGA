using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyIp : NetworkBehaviour {
    [Header("Per ottenere IP entrare in playmode")]
    public string myIP;

    public Text IPText;

    void Start ()
    {
<<<<<<< HEAD

        GetComponent<NetworkManager>().StartHost();
=======
        IPText = GameObject.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        myIP = LocalIPAddress();
        IPText.text = "  IP: " + LocalIPAddress();          
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
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
