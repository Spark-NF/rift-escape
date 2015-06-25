using UnityEngine;
using System.Collections;

public class DigicodeButton : MonoBehaviour {
	public char buttonValue;
	public GameObject doorLocked;
	private bool enabled = false;
	private LockedRoomDoor door;

	// Use this for initialization
	void Start () {
		door = doorLocked.GetComponent<LockedRoomDoor>();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>() &&
		    !enabled && LockedRoomDoor.Locked && door.DigicodePressed(buttonValue)) {
			var meshRenderer = GetComponentInChildren<TextMesh> ().GetComponentInParent<MeshRenderer> ();
			meshRenderer.material.color = Color.green;
			enabled = true;
		}
	}

	public void disable()
	{
		var meshRenderer = GetComponentInChildren<TextMesh> ().GetComponentInParent<MeshRenderer> ();
		meshRenderer.material.color = Color.black;
		enabled = false;
	}
}
