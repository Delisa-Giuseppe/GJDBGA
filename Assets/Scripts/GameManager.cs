﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public string IpAddress;
    //public string Port;

    public int maxPlayer = 2;
    public static bool startGame;
    public List<GameObject> spawnPoints;
    public List<PlayerController> playerList = new List<PlayerController>();
    public List<GameObject> healthBar;
    public List<GameObject> scorePoint;
    public List<Material> playerMat;
    public Animator countdown;
    private bool isInitialized = false;

    public int matchDeathCounter = 0;

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

    void Update()
    {
        if (!isInitialized && playerList.Count == maxPlayer)
        {
            InitializePlayer();
            countdown.SetTrigger("Start");
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
            player.mesh.GetComponent<SkinnedMeshRenderer>().material = playerMat[id];
            if (id == 1 || id == 2)
                player.helmet.SetActive(true);
            else
            {
                player.helmet.SetActive(false);
                player.transform.position = spawnPoints[id].transform.position; 
            }
        }
    }

    public void checkVictory()
    {
        if (matchDeathCounter == maxPlayer-1)
        {
            foreach (var player in playerList)
            {
                if (player.isDead == false)
                {
                    player.gamePoints += 1;
                    for (int i = 0; i < player.gamePoints; i++)
                    {
                        scorePoint[player.playerID - 1].transform.GetChild(i).GetComponent<Image>().color = new Color(0, 255, 0);
                    }

                    if (player.gamePoints >= 3)
                    {
                        playerWins(player.playerID);
                    }
                }

                player.Health = 10;
                player.isDead = false;
                player.GetComponent<Animator>().SetTrigger("Respawn");
                player.transform.position = spawnPoints[player.playerID - 1].transform.position;
                
            }
            
            matchDeathCounter = 0;
            startGame = false;
            countdown.gameObject.SetActive(true);
        }
    }

    public void playerWins(int playerID)
    {
        //SceneManager.LoadScene("Assets/Scene/Menu/Vittoria.unity");
        FindObjectOfType<NetworkManager>().ServerChangeScene("Vittoria");
    }
}
  