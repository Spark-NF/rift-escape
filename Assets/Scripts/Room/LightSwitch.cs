using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

	public GameObject light;
	public GameObject shadow;

	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter() {
		light.SetActive (!light.activeSelf);
		shadow.SetActive (!shadow.activeSelf);
	}
}
