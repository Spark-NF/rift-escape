using UnityEngine;
using System.Collections;

using Leap;

public class SwitchMode : MonoBehaviour {

	public HandController controller;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) {
			controller.isHeadMounted = false;
			controller.transform.rotation = new Quaternion(350, 180, 0, 0);
			controller.transform.position = new Vector3(0, -0.2f, 0.2f);
		}
	}
}
