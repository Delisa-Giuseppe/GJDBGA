using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public Canvas nPlayer;
    public int maxPlayer = 4;
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

    public override void OnStartServer()
    {
        nPlayer.gameObject.SetActive(true);
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

    public void TwoPlayerGame ()
    {
        netM.matchSize = 2;
    }

    public void ThreePlayerGame()
    {
        netM.matchSize = 3;
    }

    public void FourPlayerGame()
    {
        netM.matchSize = 4;
    }
}
