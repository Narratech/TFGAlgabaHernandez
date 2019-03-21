using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MicTest : MonoBehaviour {
    // Use this for initialization
    AudioSource c;
    Microphone m;
    Text txt;
	void Start () {
        txt = GameObject.Find("Aud").GetComponent<Text>();
        uint i = 0;
        foreach (var mS in Microphone.devices)
        {
            Debug.Log("Microphone number " + i.ToString() + ":" + mS);
            i++;
        }

        
        c = GetComponent<AudioSource>();
        c.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        c.Play();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
