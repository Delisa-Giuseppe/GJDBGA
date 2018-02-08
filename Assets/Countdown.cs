using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    public void StartGame()
    {
        GetComponent<AudioSource>().Stop();
        AudioSource[] audio = GameObject.Find("Audio").GetComponents<AudioSource>();
        foreach (var a in audio)
        {
            a.Play();
        }
        GameManager.startGame = true;
        Destroy(this.gameObject);
    }
}
