using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int maxPlayer = 4;
    public static bool startGame;
    public List<GameObject> spawnPoints;
    private List<PlayerController> playerList = new List<PlayerController>();
    private bool isInitialized;

    private void Start()
    {
        startGame = false;
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
}
