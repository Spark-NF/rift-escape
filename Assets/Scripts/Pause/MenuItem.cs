using UnityEngine;
using System.Collections;

public abstract class MenuItem : MonoBehaviour
{
	public KeyCode letter = 0;

	public abstract void activate();

	void OnTriggerEnter(Collider other)
	{
		activate();
	}

	void Update()
	{
		if (letter != 0 && Input.GetKeyDown(letter))
		{
			activate();
		}
	}
}
