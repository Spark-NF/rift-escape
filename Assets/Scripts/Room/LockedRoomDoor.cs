using UnityEngine;
using System.Collections;

public class LockedRoomDoor : DoorOpen {
	public AudioClip lockedsound;
	public AudioClip unlocksound;
	public static bool Locked = true;
	static bool wasLocked = true;
	
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
