using UnityEngine;
using System.Collections;

public class MenuValueOvrX : MenuValue
{
	void Start()
	{
        val = (int)(PlayerPrefs.GetFloat("ovrControlMinimum.x", OVRPlayerController.ovrControlMinimum.x) * 100);
		updateLabel();
	}

	public override void change(int oldValue, int newValue)
	{
        OVRPlayerController.ovrControlMinimum.x = (float)newValue / 100;
        PlayerPrefs.SetFloat("ovrControlMinimum.x", (float)newValue / 100);
	}
}
