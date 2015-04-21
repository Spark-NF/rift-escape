using UnityEngine;
using System.Collections;

public class QuitButtonCollider : MonoBehaviour {
	
	public MenuEvents events;
	
	void OnTriggerEnter(Collider other)
	{
		events.MenuQuit();
	}
}
