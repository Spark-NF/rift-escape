using UnityEngine;
using System.Collections;

public class MenuValueOvrY : MenuValue
{
	void Start()
	{
		val = (int)(PlayerPrefs.GetFloat("ovrControlSensitivity.y") * 100);
		updateLabel();
	}

	public override void change(int oldValue, int newValue)
	{
		OVRPlayerController.ovrControlSensitivity.y = (float)newValue / 100;
		PlayerPrefs.SetFloat("ovrControlSensitivity.y", (float)newValue / 100);
	}
}
