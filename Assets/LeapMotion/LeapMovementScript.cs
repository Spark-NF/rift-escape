using UnityEngine;
using System.Collections;

public class LeapMovementScript : MonoBehaviour {
	public OVRPlayerController.LeapMovementAction action = OVRPlayerController.LeapMovementAction.None;
    private Object[] hands = new Object[2]; // One per hand, we'll be taking the hand name as left and right have two different classes with the pause button
	// Use this for initialization
	void Start () {
        hands[0] = hands[1] = null;
    }
	
	void OnTriggerStay(Collider other) 
	{
        HandModel hand;
        if ((OVRPlayerController.nextAction & action) == 0) // action have been done already, clearing hands detection
            hands[0] = hands[1] = null;
        if (other.transform.parent && other.transform.parent.parent && (hand = other.transform.parent.parent.GetComponent<HandModel>())) // is hand
		{
            if ((OVRPlayerController.nextAction & action) == 0) // Current action is not mine : I'm replacing an action
            {
                OVRPlayerController.nextAction = action + 1;
                hands[0] = hand;
            }
            else if (hand != hands[0] && hands[1] == null) // I should only have two hands
            {
                OVRPlayerController.nextAction += 1; // movement factor ++
                hands[1] = hand;
            }
        }  
	}
}
