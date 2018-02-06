using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    public int playerID;
    public Text labelPlayer;

    [SyncVar] public int skinIndex = 0;
    // Use this for initialization
    void Start()
    {
        playerID = GameObject.FindObjectsOfType<PlayerController>().Length;
        labelPlayer.text = "PLAYER " + playerID;
        labelPlayer.transform.LookAt(Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 500);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CmdChangeSkin(skinIndex, gameObject);
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

    void PlayerMovement ()
    {
        if (isLocalPlayer)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Terrain")
            {
                Vector3 objectHit = hit.point;
                transform.DOMove(objectHit, 5);
                transform.DOLookAt(objectHit, 0.5f);
            }
        }
    }
}
