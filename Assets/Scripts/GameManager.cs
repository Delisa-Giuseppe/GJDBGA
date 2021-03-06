﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD

public class GameManager : MonoBehaviour {

    public int maxPlayer = 4;
=======
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public string IpAddress;
    //public string Port;

    public int maxPlayer = 2;
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
    public static bool startGame;
    public static int winnerID;
    public List<GameObject> spawnPoints;
<<<<<<< HEAD
    private List<PlayerController> playerList = new List<PlayerController>();
    private bool isInitialized;
=======
    public List<PlayerController> playerList = new List<PlayerController>();
    public List<GameObject> healthBar;
    public List<GameObject> scorePoint;
    public List<Material> playerMat;
    public Animator countdown;
    private bool isInitialized = false;

    public GameObject waitingText;

    public int matchDeathCounter = 0;
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016

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
        else if (!isInitialized && playerList.Count < maxPlayer)
        {
            waitingText.SetActive(true);
        }
    }

    private void InitializePlayer()
    {
        isInitialized = true;
        waitingText.SetActive(false);
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
<<<<<<< HEAD
=======

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

                    if (player.gamePoints >= 2)
                    {
                        winnerID = player.playerID;
                        playerWins();
                        return;
                    }
                }

                player.isDead = false;
                player.CmdAnimate("Respawn", false, true);
                player.transform.position = spawnPoints[player.playerID - 1].transform.position;
                player.maxHealth = 10;
                playerList[player.playerID-1].maxHealth = 10;
                if (player.weapon)
                {
                    player.maxHealth = player.weapon.Defence * player.maxHealth;
                    playerList[player.playerID - 1].maxHealth = player.weapon.Defence * playerList[player.playerID - 1].maxHealth;
                    playerList[player.playerID - 1].Health = playerList[player.playerID - 1].maxHealth;
                }
                else
                {
                    player.Health = 10;
                    playerList[player.playerID - 1].Health = 10;
                }
            }

            matchDeathCounter = 0;
        }
    }

    public void playerWins()
    {
        //SceneManager.LoadScene("Assets/Scene/Menu/Vittoria.unity");
        FindObjectOfType<NetworkManager>().ServerChangeScene("Vittoria");
    }
>>>>>>> f3fec542f616d94f1e47f04e81c8c44df1e7f016
}
  