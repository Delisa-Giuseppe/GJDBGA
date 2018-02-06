using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	[Tooltip("Imposta qui la scena che vuoi caricare")]
	public int sceneToLoad;

	public void SceneByIndex(){
		SceneManager.LoadScene (sceneToLoad);
	}

	public void LoadSceneAfter(float timeToWait ){
		Invoke ("SceneByIndex", timeToWait);
	}

	public void CloseTheGame(){
		Debug.Log ("Closing the game");
		Application.Quit ();
	}
}
