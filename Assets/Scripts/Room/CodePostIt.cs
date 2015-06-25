using UnityEngine;
using System.Collections;

public class CodePostIt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var textMesh = GetComponent<TextMesh> ();
		textMesh.text = textMesh.text.Replace ("<code>", LockedRoomDoor.code);
	}
}
