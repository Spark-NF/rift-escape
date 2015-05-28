﻿using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
	public AudioSource source = null;
	public AudioClip[] footsteps;
	public float runningInterval;
	public float walkingInterval;
	
	private CharacterController cc;
	private bool playing;
	
	void Start()	
	{
		cc = GetComponent<CharacterController>();
		playing = false;
	}
	
	void Update()	
	{
		if (cc.isGrounded == true && cc.velocity.magnitude > 0.3f && source.isPlaying == false && !playing)
		{
			source.volume = Random.Range(0.8f, 1);
			source.pitch = Random.Range(0.8f, 1.1f);
			source.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
			
			bool running = cc.velocity.magnitude > 2f;
			StartCoroutine(WaitForNextFoot(running ? runningInterval : walkingInterval));
		}
	}
	
	IEnumerator WaitForNextFoot(float wait)
	{
		playing = true;
		yield return new WaitForSeconds(wait);
		playing = false;
	}
}