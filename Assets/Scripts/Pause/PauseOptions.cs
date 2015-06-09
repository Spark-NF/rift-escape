using UnityEngine;
using System.Collections;

public class PauseOptions : MenuItem
{
	public GameObject currentPanel = null;
	public GameObject nextPanel = null;

	public override void activate()
	{
		currentPanel.SetActive(false);
		nextPanel.SetActive(true);
	}
}
