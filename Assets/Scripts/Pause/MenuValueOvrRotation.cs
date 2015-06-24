using UnityEngine;
using System.Collections;

public class MenuValueOvrRotation : MenuValue
{
	public override void change(int oldValue, int newValue)
	{
		OVRPlayerController.ovrRotationMinimum.y = newValue;
	}
}
