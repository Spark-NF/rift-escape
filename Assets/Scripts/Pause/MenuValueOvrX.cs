using UnityEngine;
using System.Collections;

public class MenuValueOvrX : MenuValue
{
	void Start()
	{
		val = (int)(PlayerPrefs.GetFloat("ovrControlSensitivity.x") * 100);
		updateLabel();
	}

	public override void change(int oldValue, int newValue)
	{
		OVRPlayerController.ovrControlSensitivity.x = (float)newValue / 100;
		PlayerPrefs.SetFloat("ovrControlSensitivity.x", (float)newValue / 100);
	}
}
