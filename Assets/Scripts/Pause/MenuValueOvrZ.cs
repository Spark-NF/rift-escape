using UnityEngine;
using System.Collections;

public class MenuValueOvrZ : MenuValue
{
	void Start()
	{
		val = (int)(PlayerPrefs.GetFloat("ovrControlSensitivity.z") * 100);
		updateLabel();
	}

	public override void change(int oldValue, int newValue)
	{
		OVRPlayerController.ovrControlSensitivity.z = (float)newValue / 100;
		PlayerPrefs.SetFloat("ovrControlSensitivity.z", (float)newValue / 100);
	}
}
