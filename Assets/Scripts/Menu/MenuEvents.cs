using UnityEngine;
using System.Collections;

public class MenuEvents : MonoBehaviour
{
	public AudioSource source = null;
	public AudioClip onPlay = null;
	public AudioClip onQuit = null;

	public void MenuQuit()
	{
		if (onQuit != null && source != null)
			StartCoroutine(WaitForAudio(onQuit));

		Application.Quit();
	}
	public void MenuPlay()
	{
		if (onPlay != null && source != null)
			StartCoroutine(WaitForAudio(onPlay));

		Application.LoadLevel("Room");
	}

	public void MenuOptions()
	{
		Debug.LogWarning("Options");
	}

	IEnumerator WaitForAudio(AudioClip sound)
	{
		source.PlayOneShot(sound);
		yield return new WaitForSeconds(sound.length);
	}
}
