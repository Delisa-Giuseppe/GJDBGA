using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public string IpAddress;
    //public string Port;

    public int maxPlayer = 2;
    public static bool startGame;
    public List<GameObject> spawnPoints;
    public List<PlayerController> playerList = new List<PlayerController>();
    public List<GameObject> healthBar;
    public List<Material> playerMat;
    private bool isInitialized = false;

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
            player.mesh.GetComponent<SkinnedMeshRenderer>().material = playerMat[id];
            if (id == 1 || id == 2)
                player.helmet.SetActive(true);
            else
                player.helmet.SetActive(false);
            player.transform.position = spawnPoints[id].transform.position;
        }
    }


    //int contatore morti
    public int DeathCounter;
    //when player muore contatore++
    void checkmorte()
    {
        foreach (var player in playerList)
        {
            if (player.isDead == true)
            {
                DeathCounter++;
            }
        }

    }



    void checkVictory()
    {  //when contatore 3   
        if (DeathCounter == 3)
        {   //check vittoria(controllare player vivo) sommare 1pt al player vivo, check pt player if 3 Vittoria
            foreach (var player in playerList)
            {
                if (player.isDead == false)
                {

                }
            }
            //altrimenti
            //contatore 0
            DeathCounter = 0;
        }
        else
        {
            playerRespawn();
        }

    }

    void playerRespawn()
    {
        //respawn player totale (10vit)
        //bool death false;
        //allocate to spawnpoint;

        for (int i = 0; i < playerList.Count - 1; i++)
        {
            playerList[i].Health = 10;
            playerList[i].isDead = true;
            playerList[i].transform.position = spawnPoints[i].transform.position;
        }

    }
}
  