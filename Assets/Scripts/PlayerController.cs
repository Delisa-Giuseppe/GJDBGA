using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    [SyncVar] public int playerID;
    public Text labelPlayer;

    private PlayerStat ps;
    private LineRenderer lr;
    private Vector3 cursorPosition;
    private Vector3 playerDirection;
    private GameManager gm;

    [SyncVar] public int skinIndex = 0;

    void Start()
    {
        ps = GetComponent<PlayerStat>();
        lr = GetComponent<LineRenderer>();
        gm = FindObjectOfType<GameManager>();
        gm.PlayerList.Add(this);
    }

    void Update()
    {
        if(GameManager.startGame)
        {
            PlayerMovement();
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
                cursorPosition = hit.point;
                cursorPosition.y += GetComponent<CapsuleCollider>().height / 2;
                playerDirection = (cursorPosition - transform.position).normalized;
                GetComponent<Rigidbody>().AddForce(playerDirection * (0.1f + (ps.PlayerSpeed * 0.03f)), ForceMode.VelocityChange);
                transform.DOLookAt(cursorPosition, 0.5f);

                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, cursorPosition);
            }
        }
    }
}
