using UnityEngine;
using System.Collections;

public class PauseOptionsToggleOvr : MenuToggle
{
	public override void toggle(bool active)
	{
		OVRPlayerController.ovrMovement = active;
	}
}
