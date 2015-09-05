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
		/*leap = hand.GetLeapController();
		leap.EnableGesture(Gesture.GestureType.TYPECIRCLE);*/
	}

	public bool togglePause()
	{
		if (Time.timeScale == 0.99f)
		{
			if (source != null && unpause != null)
			{
				source.PlayOneShot(unpause);
			}
			// Stopping movement
			OVRPlayerController.paused = false;
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
			OVRPlayerController.paused = true;
			Time.timeScale = 0.99f;
			canvas.SetActive(true);

			return true;    
		}
	}

	void Update()
	{
		/*Frame frame = leap.Frame();
		foreach (Gesture gest in frame.Gestures())
		{
			if (gest.Type == Gesture.GestureType.TYPECIRCLE)
			{
				paused = togglePause();
			}
		}*/

		if (Input.GetKeyDown(KeyCode.P))
		{
			paused = togglePause();
		}
	}
}
