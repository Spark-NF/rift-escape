using UnityEngine;
using System.Collections;

public class MenuValueOvrRotation : MenuValue
{
	void Start()
	{
		val = PlayerPrefs.GetInt("ovrRotationMinimum.y");
		updateLabel();
	}

	public override void change(int oldValue, int newValue)
	{
		OVRPlayerController.ovrRotationMinimum.y = newValue;
		PlayerPrefs.SetInt("ovrRotationMinimum.y", newValue);
	}
}
