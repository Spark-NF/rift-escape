using UnityEngine;
using System.Collections;

public class MenuValueOvrY : MenuValue
{
	void Start()
	{
        val = (int)(PlayerPrefs.GetFloat("ovrControlMinimum.y", OVRPlayerController.ovrControlMinimum.y) * 100);
		updateLabel();
	}

	public override void change(int oldValue, int newValue)
	{
        OVRPlayerController.ovrControlMinimum.y = (float)newValue / 100;
        PlayerPrefs.SetFloat("ovrControlMinimum.y", (float)newValue / 100);
	}
}
