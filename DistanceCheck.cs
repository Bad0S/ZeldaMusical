﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : MonoBehaviour {
    public Collider2D Player;
    public AudioSource BGMusic;
    public AudioClip MusicToPlay;
    public AudioClip MusicToPlayExit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    private void OnTriggerEnter2D(Collider2D Player)
    { 
		if (BGMusic.clip != MusicToPlay) 
		{
			while (BGMusic.volume > 0) 
			{
				StartCoroutine (MusicFade ());
				// BGMusic.clip = MusicToPlay;
				// BGMusic.Play();
			}
		}
    }

    private void OnTriggerExit2D(Collider2D Player)
    {
        if (BGMusic.clip != MusicToPlayExit)
        {
            BGMusic.clip = MusicToPlayExit;
            BGMusic.Play();
        }
    }

	IEnumerator MusicFade()
	{
		print (BGMusic.volume);
		BGMusic.volume -= 0.01f;
		yield return new WaitForSecondsRealtime(0.1f);
	}
}
