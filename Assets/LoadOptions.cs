using UnityEngine;
using System.Collections;

public class LoadOptions : MonoBehaviour
{
	void Start()
	{
		OVRPlayerController.LeapMovement = PlayerPrefs.GetInt("LeapMovement") == 1;
		OVRPlayerController.ovrMovement = PlayerPrefs.GetInt("ovrMovement") == 1;
		OVRPlayerController.softCrounching = PlayerPrefs.GetInt("softCrounching") == 1;
		
		OVRPlayerController.ovrControlSensitivity.x = PlayerPrefs.GetFloat("ovrControlSensitivity.x");
		OVRPlayerController.ovrControlSensitivity.y = PlayerPrefs.GetFloat("ovrControlSensitivity.y");
		OVRPlayerController.ovrControlSensitivity.z = PlayerPrefs.GetFloat("ovrControlSensitivity.z");
		OVRPlayerController.ovrRotationMinimum.y = PlayerPrefs.GetInt("ovrRotationMinimum.y");
	}
}
