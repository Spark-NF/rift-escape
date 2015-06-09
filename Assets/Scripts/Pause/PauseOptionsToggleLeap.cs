using UnityEngine;
using System.Collections;

public class PauseOptionsToggleLeap : MenuToggle
{
	public override void toggle(bool active)
	{
		OVRPlayerController.LeapMovement = active;
	}
}
