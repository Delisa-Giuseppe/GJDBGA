using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

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
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 200.0f;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);

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
