using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConnectionManager : NetworkBehaviour
{
    private string IpAddress;
    public Canvas firstCanvas;
    public Canvas btnCanvas;
    public Canvas IPCanvas;

    private int playersNumber;

    public void StartHASHost()
    {
        GetComponent<NetworkManager>().maxConnections = playersNumber - 1;
        GetComponent<NetworkManager>().StartHost();
    }

    public void StartHASClient()
    {
        GetComponent<NetworkManager>().networkAddress = IpAddress;
        GetComponent<NetworkManager>().networkPort = 7777;
        GetComponent<NetworkManager>().StartClient();
    }

    public void SetPlayerOnGame (int nPlayers)
    {
        playersNumber = nPlayers;
        StartHASHost();
    }

    public void SetIP ()
    {
        IpAddress = IPCanvas.transform.Find("InputField").GetComponent<InputField>().text;
        StartHASClient();
    }

    public void MoveToNPlayerCanvas()
    {
        firstCanvas.gameObject.SetActive(false);
        btnCanvas.gameObject.SetActive(true);
    }

    public void MoveToIPCanvas()
    {
        firstCanvas.gameObject.SetActive(false);
        IPCanvas.gameObject.SetActive(true);
    }
}
