using UnityEngine;
using System.Collections;

using Leap;

public class GrabObjects : MonoBehaviour {

	public HandController hand;

	private bool handClosed;
	private Object collidedObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (IsHandClosed () && !handClosed) {
			OnHandClose ();
		} else if (!IsHandClosed () && handClosed) {
			OnHandOpen();
		}
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log ("collision entered: " + col.ToString());
	}

	void OnHandClose() {
		handClosed = true;
		Debug.Log ("Hand closed");
	}

	void OnHandOpen() {
		handClosed = false;
		Debug.Log ("Hand opened");
	}

	bool IsHandClosed() {
		Frame frame = hand.GetFrame ();
		int ext = 0;
		foreach (Finger finger in frame.Fingers) {
			if (finger.IsExtended) {
				ext++;
			}
		}

		return ext == 0;
	}
}
