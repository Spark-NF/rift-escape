using UnityEngine;
using System.Collections;

using Leap;

public class PauseMenu : MonoBehaviour
{
	public AudioSource source = null;
	public AudioClip pause = null;
	public AudioClip unpause = null;

	public GameObject canvas;
	private bool paused = false;

	public HandController hand;
	public Controller leap;

	void Start()
	{
		//Debug.Log ("Pause");
		leap = hand.GetLeapController();
		leap.EnableGesture(Gesture.GestureType.TYPECIRCLE);
	}

	bool togglePause()
	{
		if (Time.timeScale == 0f)
		{
			if (source != null && unpause != null)
			{
				source.PlayOneShot(unpause);
			}

			Time.timeScale = 1f;
			canvas.SetActive(false);

			return false;
		}
		else
		{
			if (source != null && pause != null)
			{
				source.PlayOneShot(pause);
			}

			Time.timeScale = 0f;
			canvas.SetActive(true);

			return true;    
		}
	}

	void Update()
	{
		Frame frame = leap.Frame();
		foreach (Gesture gest in frame.Gestures())
		{
			if (gest.Type == Gesture.GestureType.TYPECIRCLE)
			{
				//paused = togglePause();
			}
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			paused = togglePause();
		}
	}
}
