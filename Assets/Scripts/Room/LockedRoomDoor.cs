using UnityEngine;
using System.Collections;

public class LockedRoomDoor : DoorOpen {
	public AudioClip lockedsound;
	public AudioClip unlocksound;
	public static bool Locked = true;
	static bool wasLocked = true;
	
	public AudioSource DigicodeSource;
	public AudioClip digicodePress;
	public AudioClip wrongCode;
	private static string actualCode = "";

	private static string code = "9637#";
	
	void OnTriggerEnter(Collider other) 
	{
		if (Locked) {
			if (lockedsound != null && !source.isPlaying)
			{
				source.clip = lockedsound;
				source.Play();
			}
		} else
			base.OpenAction (other);
	}

	public bool DigicodePressed(char c)
	{
		if (DigicodeSource != null && DigicodeSource.isPlaying)
			return false;
		actualCode += c;
		if (actualCode.Length >= 5) {
			if (actualCode != code) { // failled
				foreach (DigicodeButton button in GameObject.Find("Digicode").GetComponentsInChildren<DigicodeButton>())
					button.disable ();
				DigicodeSource.clip = wrongCode;
				DigicodeSource.Play();
				actualCode = "";
				return false; // not enabling the last button when wrong code
			} else
				Locked = false;
			actualCode = "";
		} else {
			DigicodeSource.clip = digicodePress;
			DigicodeSource.Play();
		}
		return true;
	}

	void Update () {
		if (wasLocked ^ Locked) { // state changed
			source.clip = unlocksound;
			source.Play();
			wasLocked = Locked;
		}
		if (!Locked)
			base.UpdateAction();
	}
}
