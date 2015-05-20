using UnityEngine;
using System.Collections;

public class AudioOnSceneLoad : MonoBehaviour {

	public AudioSource source;
	public AudioClip background;

	void Start()
	{
		source.clip = background;
		source.loop = true;
		source.Play();
	}
}
