using UnityEngine;
using System.Collections;

public class MenuValueOvrX : MenuValue
{
	public override void change(int oldValue, int newValue)
	{
		OVRPlayerController.ovrControlSensitivity.x = (float)newValue / 100;
		Debug.Log (OVRPlayerController.ovrControlSensitivity.x);
	}
}
