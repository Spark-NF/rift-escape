using UnityEngine;
using System.Collections;

public class PauseOptionsToggleCrouch : MenuToggle
{
	void Start()
	{
		value = PlayerPrefs.GetInt("softCrounching") == 1;
		checkmark.SetActive(value);
	}

	public override void toggle(bool active)
	{
		OVRPlayerController.softCrounching = active;
		PlayerPrefs.SetInt("softCrounching", active ? 1 : 0);
	}
}
