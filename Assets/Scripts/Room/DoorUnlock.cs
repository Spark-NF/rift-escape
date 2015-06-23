using UnityEngine;
using System.Collections;

public class DoorUnlock : MonoBehaviour {

	void OnTriggerEnter(Collider other) 
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
			LockedRoomDoor.Locked = false;
	}
}
