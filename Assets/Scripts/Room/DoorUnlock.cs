using UnityEngine;
using System.Collections;

public class DoorUnlock : MonoBehaviour {

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "BedroomUnlock")
			LockedRoomDoor.Locked = false;
	}
}
