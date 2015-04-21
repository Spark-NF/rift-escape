using UnityEngine;
using System.Collections;

public class OptionsButtonCollider : MonoBehaviour {
	
	public MenuEvents events;
	
	void OnTriggerEnter(Collider other)
	{
		events.MenuOptions();
	}
}
