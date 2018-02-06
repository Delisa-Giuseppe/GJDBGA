using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InfoManager : MonoBehaviour {
	//These are the buttons, that allow us to move between pages
	public Button forward;
	public Button backward;
	[Space]
	public GameObject page1;
	public GameObject page2;
	public GameObject page3;
	[Space]
	public int pageNumber = 1;

	// Use this for initialization
	void Start () {
		pageNumber = 1;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pageNumber == 1) {
			backward.interactable = false;
			//
			page1.SetActive (true);
			page2.SetActive (false);
			page3.SetActive (false);
		} else if (pageNumber == 2) {
			backward.interactable = true;
			forward.interactable = true;
			//
			page1.SetActive (false);
			page2.SetActive (true);
			page3.SetActive (false);
		} else if (pageNumber == 3) {
			forward.interactable = false;
			//
			page1.SetActive (false);
			page2.SetActive (false);
			page3.SetActive (true);
		}
	
	}

	public void Add(){
		pageNumber++;
	}

	public void Subtract(){
		pageNumber--;
	}

	public void Reset(){
		pageNumber = 1;
		forward.interactable = true;
		backward.interactable = false;
	}
}
