using UnityEngine;
using System.Collections;

public class PauseMenuBtn : MonoBehaviour {

	public Canvas btn;

	// Use this for initialization
	void Start () {
		Instantiate (btn);
		btn.gameObject.SetActive (true);
		btn.transform.SetParent (transform);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnDestroy() {
		DestroyObject (btn);
	}

}
