using UnityEngine;
using System.Collections;

public class LeapMovementScript : MonoBehaviour {
	public OVRPlayerController.LeapMovementAction action = OVRPlayerController.LeapMovementAction.None;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private bool IsHand(Collider other)
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
			return true;
		else
			return false;
	}
	
	void OnTriggerStay(Collider other) 
	{
		if (IsHand(other))
		{
			OVRPlayerController.nextAction = action;
		}  
	}
}
