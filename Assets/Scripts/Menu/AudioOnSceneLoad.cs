using UnityEngine;
using System.Collections;

public class AudioOnSceneLoad : MonoBehaviour {

	public AudioSource source;
	public AudioClip background;

	void Start()
	{
		audio.clip = background;
		audio.loop = true;
		audio.Play();
	}
}
