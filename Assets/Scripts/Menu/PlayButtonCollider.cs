using UnityEngine;
using System.Collections;

public class PlayButtonCollider : MonoBehaviour {

	public MenuEvents events;

	void OnTriggerEnter(Collider other)
	{
		events.MenuPlay();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            events.MenuPlay();
    }
}
