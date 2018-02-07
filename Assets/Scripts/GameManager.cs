using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public string IpAddress;
    public string Port;

    public Canvas nPlayer;
    public int maxPlayer = 1;
    public static bool startGame;
    public List<GameObject> spawnPoints;
    private List<PlayerController> playerList = new List<PlayerController>();
    private bool isInitialized;
    private NetworkManager netM;

    private void Start()
    {
        startGame = false;
        netM = GameObject.FindObjectOfType<NetworkManager>().GetComponent<NetworkManager>();
    }

    public List<PlayerController> PlayerList
    {
        get
        {
            return playerList;
        }

        set
        {
            playerList = value;
        }
    }

    void Update () {
		if(!isInitialized && playerList.Count == maxPlayer)
        {
            InitializePlayer();
            startGame = true;
        }
	}

    private void InitializePlayer()
    {
        isInitialized = true;
        foreach (var player in playerList)
        {
            int id = playerList.IndexOf(player);
            player.playerID = id + 1;
            player.labelPlayer.text = "PLAYER " + player.playerID;
            player.transform.position = spawnPoints[id].transform.position;
        }
    }

    public void SetPlayerOnGame (int nPlayers)
    {
        maxPlayer = nPlayers;
    }

    public void StartHASHost()
    {
        GetComponent<NetworkManager>().StartHost(null, maxPlayer);
    }

    public void StartHASClient()
    {
        GetComponent<NetworkManager>().networkAddress = ("192.168.65.70");
        GetComponent<NetworkManager>().networkPort = 7777;
        GetComponent<NetworkManager>().StartClient();
    }
}
