using UnityEngine;
using UnityEngine.Networking;

public class ConnectionManager : NetworkBehaviour
{
    public Canvas firstCanvas;
    public Canvas btnCanvas;

    private int playersNumber;

    public void StartHASHost()
    {
        GetComponent<NetworkManager>().maxConnections = playersNumber - 1;
        GetComponent<NetworkManager>().StartHost();
    }

    public void StartHASClient()
    {
        GetComponent<NetworkManager>().networkAddress = ("192.168.65.70");
        GetComponent<NetworkManager>().networkPort = 7777;
        GetComponent<NetworkManager>().StartClient();
    }

    public void SetPlayerOnGame (int nPlayers)
    {
        playersNumber = nPlayers;
        StartHASHost();
    }

    public void MoveToNPlayerCanvas()
    {
        firstCanvas.gameObject.SetActive(false);
        btnCanvas.gameObject.SetActive(true);
    }
}
