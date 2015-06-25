using UnityEngine;
using System.Collections;

public class PauseButtonHand : MonoBehaviour {

	bool triggered = false;

	int timer = -1;

	private bool IsHand(Collider other)
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
			return true;
		else
			return false;
	}
	
	void OnTriggerStay(Collider other) 
	{
		if (IsHand (other) && !triggered && timer == -1) {
			triggered = true;
			timer = 0;
			PauseMenu pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PauseMenu>();
			pm.togglePause ();
		}
	}

	public void Update() {
		if (timer >= 0)
			timer ++;
		if (timer >= 100)
			timer = -1;
	}

	void OnTriggerExit(Collider other) 
	{
		triggered = false;
	}
}
