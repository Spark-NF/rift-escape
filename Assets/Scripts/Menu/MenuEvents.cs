using UnityEngine;
using System.Collections;

public class MenuEvents : MonoBehaviour
{
	public AudioSource source;
	public AudioClip onPlay = null;
	public AudioClip onQuit = null;

	public void MenuQuit()
	{
		if (onQuit != null && audio != null)
			StartCoroutine(WaitForAudio(onQuit));

		Application.Quit();
	}

	public void MenuPlay()
	{
		if (onPlay != null && audio != null)
			StartCoroutine(WaitForAudio(onPlay));

		Application.LoadLevel("Room");
	}

	public void MenuOptions()
	{
		Debug.LogWarning("Options");
	}

	void WaitForAudio(AudioClip sound)
	{
		audio.PlayOneShot(sound);
		yield return new WaitForSeconds(sound.length);
	}
}
