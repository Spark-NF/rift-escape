using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public AudioSource audio = null;
	public AudioClip pause = null;
	public AudioClip unpause = null;

	public GameObject canvas;
	private bool paused = false;

	bool togglePause()
	{
		if (Time.timeScale == 0f)
		{
			if (audio != null && unpause != null)
			{
				audio.PlayOneShot(unpause);
			}

			Time.timeScale = 1f;
			canvas.SetActive(false);

			return false;
		}
		else
		{
			if (audio != null && pause != null)
			{
				audio.PlayOneShot(pause);
			}

			Time.timeScale = 0f;
			canvas.SetActive(true);

			return true;    
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			paused = togglePause();
		}
	}
}
