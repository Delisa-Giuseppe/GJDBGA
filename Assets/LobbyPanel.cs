using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour {

    public Button host;
    public Button client;

    private void Awake()
    {
        NetworkManager manager = FindObjectOfType<NetworkManager>();
        host.onClick.AddListener(() => manager.GetComponent<ConnectionManager>().SetPlayerOnGame(4));
        client.onClick.AddListener(() => manager.GetComponent<ConnectionManager>().MoveToIPCanvas());
    }
}
