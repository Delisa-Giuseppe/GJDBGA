using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<TextMeshProUGUI>().SetText("GREAT JOB Player " + GameManager.winnerID + "!");
    }
}
