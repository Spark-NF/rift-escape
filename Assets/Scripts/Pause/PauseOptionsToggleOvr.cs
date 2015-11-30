using UnityEngine;
using System.Collections;

public class PauseOptionsToggleOvr : MenuToggle
{
	void Start()
	{
		value = PlayerPrefs.GetInt("ovrMovement", 1) == 1;
		checkmark.SetActive(value);
	}

	public override void toggle(bool active)
	{
		OVRPlayerController.ovrMovement = active;
		PlayerPrefs.SetInt("ovrMovement", active ? 1 : 0);
	}
}
