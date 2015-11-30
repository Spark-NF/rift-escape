using UnityEngine;
using System.Collections;

public class PauseOptionsToggleLeap : MenuToggle
{
	void Start()
	{
		value = PlayerPrefs.GetInt("LeapMovement", 1) == 1;
		checkmark.SetActive(value);
	}

	public override void toggle(bool active)
	{
		OVRPlayerController.LeapMovement = active;
		PlayerPrefs.SetInt("LeapMovement", active ? 1 : 0);
	}
}
