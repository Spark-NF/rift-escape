using UnityEngine;
using System.Collections;

public abstract class MenuItem : MonoBehaviour
{
	public KeyCode letter = 0;
	protected float fireRate = 1.0f;
	protected static float nextFire = 0.0f;

	public static bool activated = false;

	public abstract void activate();

	void OnTriggerEnter(Collider other)
	{
		if (!activated && other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>() && Time.time > nextFire)
	    {
			activate();
			nextFire = Time.time + fireRate;
			activated = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		activated = false;
	}

	void OnTriggerStay(Collider other)
	{
		activated = true;
	}

	void Update()
	{
		// TODO fix double fire on activation
		if (letter != 0 && Input.GetKeyDown(letter))
		{
			activate();
		}
	}
}
