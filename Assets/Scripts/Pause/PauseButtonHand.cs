using UnityEngine;
using System.Collections;

public class PauseButtonHand : MonoBehaviour {

	bool triggered = false;

	private bool IsHand(Collider other)
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
			return true;
		else
			return false;
	}
	
	void OnTriggerStay(Collider other) 
	{
		if (IsHand (other) && !triggered) {
			triggered = true;
			PauseMenu pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PauseMenu>();
			pm.togglePause ();
		}
	}

	void OnTriggerExit(Collider other) 
	{
		triggered = false;
	}
}
