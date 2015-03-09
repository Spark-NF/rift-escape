using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

	public GameObject light;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter() {
		light.SetActive (!light.activeSelf);
	}
}
