using UnityEngine;
using System.Collections;

public class MenuValueOvrZ : MenuValue
{
	void Start()
	{
        val = (int)(PlayerPrefs.GetFloat("ovrControlMinimum.z", OVRPlayerController.ovrControlMinimum.z) * 100);
		updateLabel();
	}

	public override void change(int oldValue, int newValue)
	{
        OVRPlayerController.ovrControlMinimum.z = (float)newValue / 100;
        PlayerPrefs.SetFloat("ovrControlMinimum.z", (float)newValue / 100);
	}
}
