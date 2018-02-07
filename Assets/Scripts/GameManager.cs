﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    //public string IpAddress;
    //public string Port;

    public int maxPlayer = 2;
    public static bool startGame;
    public List<GameObject> spawnPoints;
    public List<PlayerController> playerList = new List<PlayerController>();
    private bool isInitialized = false;
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

    void Update ()
    {
        if (!isInitialized && playerList.Count == maxPlayer)
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