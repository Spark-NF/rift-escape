using UnityEngine;
using System.Collections;

public class PauseOptionsToggleCrouch : MenuToggle
{
	public override void toggle(bool active)
	{
		OVRPlayerController.softCrounching = active;
	}
}
