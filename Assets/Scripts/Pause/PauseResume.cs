using UnityEngine;
using System.Collections;

public class PauseResume : MenuItem
{
	public GameObject pauseController = null;

	public override void activate()
	{
		PauseMenu pm = pauseController.GetComponent<PauseMenu>();
		pm.togglePause();
	}
}
