using UnityEngine;
using System.Collections;

public class MenuValueOvrZ : MenuValue
{
	public override void change(int oldValue, int newValue)
	{
		OVRPlayerController.ovrControlSensitivity.z =  (float)newValue / 100;
	}
}
