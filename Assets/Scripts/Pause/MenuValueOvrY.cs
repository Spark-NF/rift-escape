using UnityEngine;
using System.Collections;

public class MenuValueOvrY : MenuValue
{
	public override void change(int oldValue, int newValue)
	{
		OVRPlayerController.ovrControlSensitivity.y =  (float)newValue / 100;
	}
}
