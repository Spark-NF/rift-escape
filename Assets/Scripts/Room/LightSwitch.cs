using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

	public GameObject myLight;
	public GameObject shadow;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter() {
		myLight.SetActive (!myLight.activeSelf);
		shadow.SetActive (!shadow.activeSelf);
	}
}
