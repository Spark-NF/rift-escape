using UnityEngine;
using System.Collections;

public abstract class MenuItem : MonoBehaviour
{
	public KeyCode letter = 0;
	protected float fireRate = 1.0f;
	protected float nextFire = 0.0f;

	public abstract void activate();

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>() && Time.time > nextFire)
	    {
			activate();
			nextFire = Time.time + fireRate;
		}
	}

	void Update()
	{
		if (letter != 0 && Input.GetKeyDown(letter))
		{
			activate();
		}
	}
}
