using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseOptions : MenuItem
{
	public GameObject currentPanel = null;
	public GameObject nextPanel = null;
	public EventSystem eventSystem = null;

	public override void activate()
	{
		currentPanel.SetActive(false);
		nextPanel.SetActive(true);

		if (eventSystem != null)
			eventSystem.SetSelectedGameObject(nextPanel, null);
	}
}
