﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public Camera camera;

    [SyncVar] public int skinIndex = 0;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Terrain")
            {
                Vector3 objectHit = hit.point;
                transform.DOMove(objectHit, 5);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 500);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                CmdChangeSkin(skinIndex, gameObject);
            }


        }

    }

    [Command]
    void CmdChangeSkin(int skinIndex, GameObject player)
    {
        if (skinIndex == 0)
        {
            skinIndex = 1;
        }
        else if (skinIndex == 1)
        {
            skinIndex = 0;
        }

        RpcChangeSkin(skinIndex, player);

    }

    [ClientRpc]
    void RpcChangeSkin(int skinIndex, GameObject player)
    {
        if (skinIndex == 0)
        {
            player.transform.GetChild(1).gameObject.SetActive(false);
            player.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (skinIndex == 1)
        {
            player.transform.GetChild(0).gameObject.SetActive(false);
            player.transform.GetChild(1).gameObject.SetActive(true);
        }

    }
}
